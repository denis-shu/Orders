import { NgModule, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule, BrowserXhr } from '@angular/http';
import { sharedConfig } from './app.module.shared';
import { VenicleService } from "./make/venicle.service";
import { AppErrorHandler } from "./app.error-handler";
import * as Raven from "raven-js";
import { ImageService } from "./make/image.service";
import { BrowserXhrService, ProgressService } from "./make/progress.service";

Raven.config('https://255952418e5f4b6b9fbdf4c08ee67ebc@sentry.io/191896').install();

@NgModule({
    bootstrap: sharedConfig.bootstrap,
    declarations: sharedConfig.declarations,
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        ...sharedConfig.imports
    ],
    providers: [
        VenicleService,
        ImageService,
        { provide: 'ORIGIN_URL', useValue: location.origin },
        { provide: ErrorHandler, useClass: AppErrorHandler },
        { provide: BrowserXhr, useClass: BrowserXhrService },
        ProgressService
    ]
})
export class AppModule {
}
