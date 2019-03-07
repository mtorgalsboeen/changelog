import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Changelog } from '../_models/changelog';
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({
    'Authorization': 'Bearer ' + localStorage.getItem('token')
  })
};

@Injectable({
  providedIn: 'root'
})
export class ChangelogService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) {}
getUsers(): Observable<Changelog[]> {
  return this.http.get<Changelog[]>(this.baseUrl + 'changelogs', httpOptions);
}

getUser(id): Observable<Changelog> {
  return this.http.get<Changelog>(this.baseUrl + 'users/' + id, httpOptions);
}
}
