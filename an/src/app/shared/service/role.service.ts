import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Role } from '../model/role.model';

@Injectable()
export class RoleService {
    BaseURL = 'https://localhost:44355/api/roles'

    constructor(private http: HttpClient) {}

    getAll(): Observable<Role[]>{
        return this.http.get<Role[]>('https://localhost:44355/api/roles');
    }

    get(id: number): Observable<Role>{
        return this.http.get<Role>(`${this.BaseURL}/${id}`);
    }

    create(role: Role): Observable<Role>{
        return this.http.post(this.BaseURL, role);
    }

    update(role: Role): Observable<Role>{
        return this.http.put(this.BaseURL, role);
    }

    delete(id: number): Observable<any>{
        return this.http.delete<any>(`${this.BaseURL}/${id}`);
    }


}