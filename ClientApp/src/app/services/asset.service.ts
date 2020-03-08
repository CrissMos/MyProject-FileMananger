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

    getAssetsWithThumbnailView(folderId: string): Observable<any> {
        return this.http.get<Asset[]>(this.baseUrl + "/thumbnail-view/" + folderId).pipe(
            tap(data => console.log(data)),
            catchError(this.handleError)
        );
    }

    upload(formData, folderId: string) {
        return this.http.post<any>(this.baseUrl + "/upload/" + folderId, formData).pipe(
            tap(data => console.log(data)),
            catchError(this.handleError)
        );
    }

    private handleError(error: any) {
        console.error(error);
        return throwError(error);
    }
}

