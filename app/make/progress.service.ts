import { Injectable } from '@angular/core';
import { Subject } from "rxjs/Subject";
import { BrowserXhr } from "@angular/http";

@Injectable()
export class ProgressService {
    private uploadProject: Subject<any>;

    createUploadProject() {
        this.uploadProject = new Subject();
        return this.uploadProject;
    }

    notify(progress) {
        this.uploadProject.next(progress);
    }

    complete() {
        this.uploadProject.complete();
    }
}

@Injectable()
export class BrowserXhrService extends BrowserXhr {
    constructor(private service: ProgressService) { super(); }

    build(): XMLHttpRequest {
        var xhr: XMLHttpRequest = super.build();

        
        xhr.upload.onprogress = (event) => {
            this.service.notify(this.createProgress(event));
        };

        xhr.upload.onloadend = () => {
            this.service.complete();

        }

        return xhr;
    }

    private createProgress(event) {
        return {
            total: event.total,
            percentage: Math.round(event.loaded / event.total * 100)
        }
    }
}