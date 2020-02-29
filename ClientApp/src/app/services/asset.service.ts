import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Asset } from '../models/asset.model';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
	providedIn: 'root'
})

export class AssetService {
	private baseUrl = "api/assets";

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


    addAsset(asset: Asset): Observable<Asset> {
        asset.id = null;
        return this.http.post<any>(this.baseUrl + "/create", asset, this.httpOptions)
            .pipe(
                tap(data => console.log(data)),
                catchError(this.handleError)
            );
    }

    getAssets(): Observable<any> {
        return this.http.get<Asset[]>(this.baseUrl + "/get-assets").pipe(
            tap(data => console.log(data)),
            catchError(this.handleError)
        );
    }

    //addAsset(asset: Asset): Observable<Asset> {
    //    return this.http.post<Asset>(this.baseUrl + "/create", asset);
    //}

    //private handleError<T>(operation = 'operation', result?: T) {
    //    return (error: any): Observable<T> => {

    //        // TODO: send the error to remote logging infrastructure
    //        console.error(error); // log to console instead

    //        // TODO: better job of transforming error for user consumption
    //        console.log(`${operation} failed: ${error.message}`);

    //        // Let the app keep running by returning an empty result.
    //        return of(result as T);
    //    };
    //}
    private handleError(error: any) {
        console.error(error);
        return throwError(error);
    }
}

