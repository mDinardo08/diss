import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs/Observable";

@Injectable()
export class ApiService {

    constructor(private http: HttpClient) {}

    getURL(): String {
        return "./api/";
    }

    getOptions() {
        return {headers: new HttpHeaders({
            "Content-Type": "application/json"
        })};
    }

    get<T>(endPoint: string): Observable<T> {
        return this.http.get<T>(this.getURL() + endPoint);
    }

    post<T>(endPoint: string, data: any): Observable<T> {
        return this.http.post<T>(this.getURL() + endPoint, data, this.getOptions());
    }

}
