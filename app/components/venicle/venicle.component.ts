import * as _ from 'underscore';
import { Component, OnInit } from '@angular/core';
import { VenicleService } from "../../make/venicle.service";
import { ToastyService } from "ng2-toasty";
import { Router, ActivatedRoute } from "@angular/router";
import { Observable } from "rxjs/Observable";
import "rxjs/add/Observable/forkJoin";
import { SaveVenicle, Venicle } from "../../Models/venicle";


@Component({
    selector: 'app-venicle',
    templateUrl: './venicle.component.html',
    providers: [VenicleService]
})
export class VenicleComponent implements OnInit {
    makes: any[];
    models: any[];
    features: any[];
    venicle: SaveVenicle = {
        id: 0,
        modelId: 0,
        makeId: 0,
        isRegistered: false,
        features: [],
        contact: {
            name: '',
            phone: '',
            ameil: ''
        }
    };

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private venicleService: VenicleService,
        private toastyService: ToastyService
    ) {
        route.params.subscribe(p => {
            this.venicle.id = +p['id'] || 0;
        });
    }

    ngOnInit() {
        var sources = [
            this.venicleService.getFeatures(),
            this.venicleService.getMakes()
        ];

        if (this.venicle.id)
            sources.push(this.venicleService.getVenicle(this.venicle.id));

        Observable.forkJoin(sources).subscribe(data => {
            this.makes = data[1];
            this.features = data[0];
            if (this.venicle.id) {
                this.SetVenicle(data[2]);
                this.populateModels();
            }
        },
            err => {
                if (err.status == 404)
                    this.router.navigate(['/home']);
            }
        );



        //if (!Number.isNaN(this.venicle.id)) {
        //    this.venicleService.getVenicle(this.venicle.id)
        //        .subscribe(v => {
        //            this.venicle = v;
        //        }
        //        , err => {
        //            if (err.status == 404)
        //                this.router.navigate(['/home']);
        //        }
        //        );
        //}

        //this.venicleService.getMakes().subscribe(makes =>
        //    this.makes = makes
        //);
        //this.venicleService.getFeatures().subscribe(f =>
        //    this.features = f
        //);

    }

    private SetVenicle(v: Venicle) {
        this.venicle.id = v.id;
        this.venicle.makeId = v.make.id;
        this.venicle.modelId = v.model.id;
        this.venicle.isRegistered = v.isRegistered;
        this.venicle.contact = v.contact;
        this.venicle.features = _.pluck(v.features, 'id');
    }

    onMakeChange() {
        this.populateModels();
        delete this.venicle.modelId;
    }

    private populateModels() {
        var selectedMake = this.makes.find(m => m.id == this.venicle.makeId);
        this.models = selectedMake ? selectedMake.models : [];
    }


    onFeatureToggle(featureId, $event) {
        if ($event.target.checked)
            this.venicle.features.push(featureId);
        else {
            var index = this.venicle.features.indexOf(featureId);
            this.venicle.features.splice(index, 1);
        }
    }

    onSubmit() {

        var result$ = (this.venicle.id) ? this.venicleService.update(this.venicle) :
            this.venicleService.create(this.venicle);
        //this.venicleService.update(this.venicle);
        result$.subscribe(venicle => {
            this.toastyService.success({
                title: 'Success',
                msg: 'Data was sucessfully saved.',
                theme: 'bootstrap',
                showClose: true,
                timeout: 5000
            });
            this.router.navigate(['/venicles/', venicle.id])                    
        });
        //if (this.venicle.id) {
        //    this.venicleService.update(this.venicle)
        //        .subscribe(x => {
        //            this.toastyService.success({
        //                title: 'Success',
        //                msg: 'Was updated',
        //                theme: 'bootstrap',
        //                showClose: true,
        //                timeout: 5000
        //            });
        //        });
        //}
        //else {
        //    this.venicleService.create(this.venicle)
        //        .subscribe(
        //        x => this.venicle = x);
        //}
    }
    delete() {
        if (confirm("Are u sure?")) {
            this.venicleService.delete(this.venicle.id)
                .subscribe(x => {
                    this.router.navigate(['/home']);
                });
        }
    }
}