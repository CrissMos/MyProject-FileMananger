import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Folder } from '../models/folder.model';

@Injectable({
    providedIn: 'root'
})

export class FolderService {
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


    addFolder(folder: Folder): Observable<Folder> {
        folder.id = null;
        return this.http.post<any>(this.baseUrl + "/create-folder", folder, this.httpOptions)
            .pipe(
                tap(data => console.log(data)),
                catchError(this.handleError)
            );
    }

    getFolders(): Observable<any> {
        return this.http.get<Folder[]>(this.baseUrl + "/get-folders").pipe(
            tap(data => console.log(data)),
            catchError(this.handleError)
        );
    }

    private handleError(error: any) {
        console.error(error);
        return throwError(error);
    }
}

