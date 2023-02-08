import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { User } from '../_model/user';

@Injectable({
  providedIn: 'root'
})
export class AccountServiceService {
  baseUrl='https://localhost:5001/api/';

  //creating a special type of observable so that other components can refer this to know if the user is logged in or not
  private currentuSource = new  BehaviorSubject<User | null>(null); //inital value of null because we cant know if user is logged in unless checked in the browser local storage, <User|null> - | is the pipe key which is like and oR heree
  currentUser$=this.currentuSource.asObservable();

  constructor(private http:HttpClient) { }

  login(model:any){    //using the interface to mention the type
    //post<user> we have to tell the type of observable we get from the api
    return this.http.post<User>(this.baseUrl+'account/login',model).pipe( map((response:User)=>{     //using the observable even before the compoent subs to store in the borwser storage
      const user = response;
      if(user){
        localStorage.setItem('user',JSON.stringify(user))     //storing in browser storage
        this.currentuSource.next(user); //setting the next value as the user to the observable to let it know user has logged in
        }
    })
    )
  }
  logout(){
    localStorage.removeItem('user');
    this.currentuSource.next(null);
  }
  setCurrentUser(user:User){
    this.currentuSource.next(user);
  }
}
