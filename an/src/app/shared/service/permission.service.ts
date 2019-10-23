import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http'
import { Observable } from 'rxjs';
import { Permission } from '../model/permission.model'

@Injectable()
export class PermissionService {
    BaseURL = 'https://localhost:44355/api/permissions'

    constructor(private http: HttpClient) {}

    getAll(): Observable<Permission[]>{
        return this.http.get<Permission[]>('https://localhost:44355/api/permissions');
    }

    get(id: number): Observable<Permission>{
        return this.http.get<Permission>(`${this.BaseURL}/${id}`);
    }

    create(permission: Permission): Observable<Permission>{
        return this.http.post(this.BaseURL, permission);
    }

    update(permission: Permission): Observable<Permission>{
        return this.http.put(this.BaseURL, permission);
    }

    delete(id: number): Observable<any>{
        return this.http.delete<any>(`${this.BaseURL}/${id}`);
    }


}