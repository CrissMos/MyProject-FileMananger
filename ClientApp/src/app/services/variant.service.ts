import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Variant } from '../models/variant.model';

@Injectable({
    providedIn: 'root'
})

export class VariantService {
    private baseUrl = "api/folders";

    headers = new HttpHeaders().set('Content-Type', 'application/json');
    httpOptions = {
        headers: this.headers
    };

    constructor(
        private http: HttpClient
    ) {
    }

    //ngOnInit() {
    //    this.getAssets();
    //}

    private handleError(error: any) {
        console.error(error);
        return throwError(error);
    }
}

