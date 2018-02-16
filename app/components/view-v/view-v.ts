
import { Component, OnInit, ElementRef, ViewChild, NgZone } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastyService } from "ng2-toasty";
import { VenicleService } from "../../make/venicle.service";
import { ImageService } from "../../make/image.service";
import { ProgressService } from "../../make/progress.service";

@Component({
    selector: 'app-viewv',
    templateUrl: 'view-v.html',
    providers: [VenicleService]
})
export class ViewVComponent implements OnInit {
    @ViewChild('fileInput') fileInput: ElementRef;
    venicle: any;
    venicleId: number;
    images: any[];
    progress: any;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private toasty: ToastyService,
        private venicleService: VenicleService,
        private imageService: ImageService,
        private progressService: ProgressService,
        private zone: NgZone
    ) {
        route.params.subscribe(p => {
            this.venicleId = +p['id'];
            if (isNaN(this.venicleId) || this.venicleId <= 0) {
                router.navigate(['/venicles']);

                return;
            }
        });
    }

    ngOnInit() {
        this.imageService.getImages(this.venicleId)
            .subscribe(images => this.images = images);

        this.venicleService.getVenicle(this.venicleId)
            .subscribe(v => this.venicle = v,
            err => {
                if (err.status == 404) {
                    this.router.navigate(['/venicles']);
                    return;
                }
            });
    }
    delete() {
        if (confirm("Are you sure?")) {
            this.venicleService.delete(this.venicle.id)
                .subscribe(x => {
                    this.router.navigate(['/venicles']);
                });
        }
    }

    uploadImage() {
        var element: HTMLInputElement = this.fileInput.nativeElement;

        this.progressService.createUploadProject()
            .subscribe(pro => {
                console.log(pro);

                this.zone.run(() => {
                    this.progress = pro;
                });
            },
            null,
            () => { this.progress = null });

        this.imageService.upload(this.venicleId, element.files[0])
            .subscribe(image => {
                this.images.push(image);
            });
    }
}