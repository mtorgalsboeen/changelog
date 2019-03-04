import { Component,ViewChild, ElementRef} from '@angular/core';
import { Customer } from './customer.model';
import { NgForm, AbstractControl } from '@angular/forms';
import { CustomHttpClientService } from '../services/custom-http-client-service';

@Component({
  selector: 'app-add-change-customer',
  templateUrl: './addcustomer.component.html',
  styleUrls: ['./addcustomer.component.css']
})

/**
 * Uses ngForm to add a customer.
 * The fields are validated either when the field looses focus (the onBlur-method/the blur-event) or when the user 
 * presses the submit-button
 */

export class AddcustomerComponent{
  @ViewChild("name") nameElem:ElementRef
  @ViewChild("urlInfo") urlInfoElem:ElementRef;
  
  readonly ADDED_MSG_DURATION = 1000;
  readonly MSG_URL_INVALID_FORMAT = "Invalid url-format";
  readonly MSG_URL_LOADING_IMG = "Loading image...";
  readonly MSG_URL_NOT_POINT_TO_IMG = "The url does not point to an image";

  readonly urlRegex = /^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$/

  customer:Customer;
  nameEmpty = false;
  urlEmpty = false;

  //Since we havent tried to load the image, both are false
  foundImageInURL = false;
  notFoundImageInURL = false;

  showAddedMsg = false;


  constructor(private httpClient:CustomHttpClientService) { 
    this.customer = new Customer();
  }

  //Called when the image-element enters the DOM, witch also means that the image starts loading
   imgOnViewportChange(imgInViewport:boolean){
    this.urlInfoElem.nativeElement.innerHTML = this.MSG_URL_LOADING_IMG;
  }
  
  //Called by an img-element when the logo from the url is a valid logo
  //See the asociated template-file for more information*/
  imgFound(){
    this.foundImageInURL = true; 
     this.urlInfoElem.nativeElement.innerHTML = "";
  }

  imgNotFound(){
     this.notFoundImageInURL = true;
     this.urlInfoElem.nativeElement.innerHTML = this.MSG_URL_NOT_POINT_TO_IMG;
  }

  //Validates and possibly show an error for the input when input looses focus 
  onBlur(formElem:NgForm){
    switch(formElem.name){
      case "name":
        this.nameEmpty = !formElem.value;
        break;
      case "logoURL":
        this.urlEmpty = !formElem.value;
        if(!formElem.valid)
          this.urlInfoElem.nativeElement.innerHTML = this.MSG_URL_INVALID_FORMAT;
        break;
      default:
        console.error(`missing handling of blur event for: ${formElem.name}`);
    }
  }


  //The form has to be valid before submit. In addition the image in the url needs to have loaded (imgFound has to be called) 
  onSubmit(form:NgForm){
    let formControls:{ [key: string]:AbstractControl; } = form.controls;

    let nameFormControl = formControls["name"];
    let logoURLFormControl = formControls["logoURL"];

    this.nameEmpty = nameFormControl.value == undefined;
    this.urlEmpty = logoURLFormControl.value == undefined;

    if(form.valid && this.foundImageInURL){
      this.tryRegistrate();
    }

  }

  private tryRegistrate(){

  }
  private onSucessfulRegistration(form:NgForm){
      this.showAddedMsg = true;
      setTimeout( () => {
        this.showAddedMsg = false;
      },this.ADDED_MSG_DURATION);

      form.resetForm();
      this.nameElem.nativeElement.focus();
  }


}
