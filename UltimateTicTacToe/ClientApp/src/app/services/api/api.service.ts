import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs/Observable";

@Injectable()
export class ApiService {
    constructor(private http: HttpClient) {}

    getURL(): String {
        return "./api/";
    }

    get<T>(endPoint: string): Observable<T> {
        return this.http.get<T>(this.getURL() + endPoint);
    }


}
