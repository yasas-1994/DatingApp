import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
 baseUrl = 'http://localhost:5000/api/auth/';

constructor(private http: HttpClient) { }
// login method will return the token as the  response. following code will save the returned token in a local storage.
// model is the object that we are passing from our nav component or nav bar.
login(model: any){
  return this.http.post(this.baseUrl + 'login', model)
  .pipe(map((response: any) => {
        const user = response;
        if (user)
        {
            localStorage.setItem('token', user.token);

        }}));

}
register(model: any){
  return this.http.post(this.baseUrl + 'register', model);

}
}
