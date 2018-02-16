import { Injectable } from '@angular/core';
import { Http } from "@angular/http";

@Injectable()
export class ImageService {

  constructor(private http: Http) { }
  upload(venicleId, image) {
      var formData = new FormData();
      formData.append('fileImage', image);
      return this.http.post(`/api/venicles/${venicleId}/images`, formData)
          .map(res => res.json());
  }

  getImages(venicleId) {
      return this.http.get(`/api/venicles/${venicleId}/images`)
          .map(res => res.json());
  }

}
