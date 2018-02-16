import { Component, OnInit } from "@angular/core";
import { Venicle, KeyValuePair } from "../../Models/venicle";
import { VenicleService } from "../../make/venicle.service";
import { Observable } from "rxjs/Observable";

@Component({
    selector: 'app-veniclelist',
    templateUrl: 'venicle-list.html',
    providers: [VenicleService]
})
export class VenicleListComponent implements OnInit {
    private readonly PAGE_SIZE = 3;

    queryResult: any = {};
    makes: KeyValuePair[];
    query: any = {
        pageSize: this.PAGE_SIZE
    };
    columns = [
        { title: 'Id' },
        { title: 'Contact Name', key: 'contactName', isSortable: true },
        { title: 'Make', key: 'make', isSortable: true },
        { title: 'Model', key: 'model', isSortable: true },
        {}
    ];

    constructor(private venicleService: VenicleService) {
    }

    ngOnInit() {
        this.venicleService.getMakes()
            .subscribe(makes => this.makes = makes);
        this.populateVenicles();
    }

    populateVenicles() {
        this.venicleService.getVenicles(this.query)
            .subscribe(result => this.queryResult = result);
    }

    onFilterChange() {
        this.query.page = 1;
        this.populateVenicles();
    }

    resetFilter() {
        this.query = {
            page: 1,
            pageSize: this.PAGE_SIZE
        };
        this.populateVenicles();
    }

    sortBy(colName) {
        if (this.query.sortBy === colName)
            this.query.isSort = !this.query.isSort;

        else {
            this.query.sortBy = colName;
            this.query.isSort = true;
        }

        this.populateVenicles();
    }

    onPageChange(page) {
        this.query.page = page;
        this.populateVenicles();
    }
}