import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ToastyModule } from 'ng2-toasty';

import { AppComponent } from './components/app/app.component'
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';
import { FetchDataComponent } from './components/fetchdata/fetchdata.component';
import { CounterComponent } from './components/counter/counter.component';
import { VenicleComponent } from './components/venicle/venicle.component';
import { VenicleListComponent } from "./components/venicle-list/venicle-list";
import { PaginationComponent } from "./components/shared/pagination.component";
import { ViewVComponent } from "./components/view-v/view-v";



export const sharedConfig: NgModule = {
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        CounterComponent,
        FetchDataComponent,
        HomeComponent,
        VenicleComponent,
        VenicleListComponent,
        PaginationComponent,
        ViewVComponent

    ],
    imports: [
        FormsModule,
        ToastyModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'venicles', pathMatch: 'full' },
            { path: 'venicles/new', component: VenicleComponent },
            { path: 'venicles/edit/:id', component: VenicleComponent },
            { path: 'venicles/:id', component: ViewVComponent },
            { path: 'venicles', component: VenicleListComponent },
            { path: 'home', component: HomeComponent },
            { path: 'counter', component: CounterComponent },
            { path: 'fetch-data', component: FetchDataComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ],
    providers: [
       
    ]
};
