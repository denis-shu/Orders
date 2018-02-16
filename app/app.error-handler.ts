
import * as Raven from "raven-js";
import { ErrorHandler, Inject, NgZone, isDevMode } from "@angular/core";
import { ToastyService } from "ng2-toasty";

export class AppErrorHandler implements ErrorHandler {

    constructor( @Inject(ToastyService) private toastyService: ToastyService,
        private ngzone: NgZone) { }

    handleError(error: any): void {
        if (!isDevMode())
            Raven.captureException(error.originalError || error);
        else


            this.ngzone.run(() => {
                console.log("Error");
                this.toastyService.error({
                    title: 'Error',
                    msg: 'An unexpected error happend',
                    theme: 'bootstrap',
                    showClose: true,
                    timeout: 5000
                });
            })

    }


}