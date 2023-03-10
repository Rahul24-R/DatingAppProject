import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_model/user';
import { AccountServiceService } from './_Services/account-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Dating App';
  users:any;

  /**
   * constructor to instanciate the HttpClient when the component is called
   */
  constructor(private http:HttpClient,private accountservice:AccountServiceService) { //injecting the account service
  }

  /**
   * is like an interface/lifecycle hook which has initialization code , even after the class is instanciated
   */
  ngOnInit(): void {

    this.http.get("https://localhost:5001/api/users").subscribe ({    // we have to subscribe to get the response from the api call
      next:response =>this.users=response,    //what next to do with the response
      error:error=>console.log(error),        //what to do on error
      complete:() => console.log("complted")  //what to do on complete
    });
    this.setCurrentUser();    // cehciking is a user is currently logged in from the local broweser
  }

  setCurrentUser(){
    const userString= (localStorage.getItem('user'));
    if(!userString){
      return;
    }
    const user:User = JSON.parse(userString);
    this.accountservice.setCurrentUser(user);
  }
}
