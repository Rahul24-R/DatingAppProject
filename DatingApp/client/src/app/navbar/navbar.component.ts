import { Component, OnInit } from '@angular/core';
import { AccountServiceService } from '../_Services/account-service.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  model:any ={}
  loggedIn=false;

  constructor(private accountservice:AccountServiceService) { } //mentioning the service we are injecting

  ngOnInit(): void {
    this.getCurrentUser();  // checking if the user is already logged in
  }
 getCurrentUser(){
  this.accountservice.currentUser$.subscribe({
    next:user=>this.loggedIn=!!user,    //this !! turn it to a boolen , if there is a user it returns true
    error:err=>console.log(err),
  })
 }
  login(){
    this.accountservice.login(this.model).subscribe({
      next:res =>{
        console.log(res),
        this.loggedIn=true;
      },
      error:err=> console.log(err)
    })
  }

  logout(){
    this.accountservice.logout();
    this.loggedIn=false;
  }

}
