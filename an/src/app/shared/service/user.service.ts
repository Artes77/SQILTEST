import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Observable } from 'rxjs';
import { User, IUser } from '../model/user.model'

@Injectable()
export class UserService {
    BaseURL = 'https://localhost:44355/api/users'

    constructor(private http: HttpClient) {}

    getAll(): Observable<User[]>{
        return this.http.get<User[]>('https://localhost:44355/api/users');
    }

    get(id: number): Observable<User>{
        return this.http.get<User>(`${this.BaseURL}/${id}`);
    }

    create(user: IUser): Observable<IUser>{
        return this.http.post<IUser>(this.BaseURL, user);
    }

    update(user:User): Observable<User>{
        return this.http.put(this.BaseURL, user);
    }

    delete(id: number): Observable<any>{
        return this.http.delete<any>(`${this.BaseURL}/${id}`);
    }


}