import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { SaveVenicle } from "../Models/venicle";

@Injectable()
export class VenicleService {
    constructor(private http: Http,
        @Inject('ORIGIN_URL') private origin: string) { }

    getMakes() {
        return this.http.get('http://localhost:53333/api/makes').map(
            res => res.json());
    }

    getFeatures() {
        return this.http.get('http://localhost:53333/api/features').map(res =>
            res.json());
    }

    create(venicle) {
        return this.http.post(this.origin + '/api/venicles', venicle)
            .map(res => res.json());
    }

    getVenicle(id) {
        // if (!Number.isNaN(id))
        return this.http.get(this.origin + '/api/venicles/' + id)
            .map(res => res.json());
    }

    update(venicle: SaveVenicle) {
        return this.http.put('/api/venicles/' + venicle.id, venicle)
            .map(res => res.json());
    }

    delete(id) {
        return this.http.delete('/api/venicles/' + id)
            .map(res => res.json());
    }

    getVenicles(filter) {
        return this.http.get(this.origin + '/api/venicles' + '?' + this.toQueryString(filter))
            .map(res => res.json());
    }

    toQueryString(object) {
        var parts = [];
        for (var prop in object) {
            var value = object[prop];
            if (value != null && value != undefined)
                parts.push(encodeURIComponent(prop) + '=' + encodeURIComponent(value)); 
        }

        return parts.join('&');
    }
}