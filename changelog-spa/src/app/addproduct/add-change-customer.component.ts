import { Component,ViewChild, ElementRef} from '@angular/core';
import { Customer } from '../../model/customer.model';
import { NgForm, AbstractControl } from '@angular/forms';
import { CustomHttpClientService } from '../../services/custom-http-client-service';

@Component({
  selector: 'app-add-change-customer',
  templateUrl: './add-change-customer.component.html',
  styleUrls: ['./add-change-customer.component.css']
})

/**
 * Uses ngForm to add a customer.
 * The fields are validated either when the field looses focus (the onBlur-method/ the blur-event) or when the user 
 * presses the submit-button
 */

export class AddChangeCustomerComponent{
  @ViewChild("name") nameElem:ElementRef
  @ViewChild("urlError") urlErrorElem:ElementRef;
  
  customer:Customer;
  nameEmpty = false;
  urlEmpty = false;

  //Since we havent tried to load the image, both are false
  foundImageInURL = false;
  notFoundImageInURL = false;

  showAddedMsg = false;

  readonly ADDED_MSG_DURATION = 1000;
  readonly MSG_URL_INVALID_FORMAT = "Invalid url-format";
  readonly MSG_URL_NOT_POINT_TO_IMG = "The url does not point to an image";

  readonly urlRegex = /^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$/

  constructor(private httpClient:CustomHttpClientService) { 
    this.customer = new Customer();
  }

  
  /*Called by an img-element when the logo from the url is a valid logo, or the logo could not be loaded.*/ 
  /*See the asociated template-file for more information*/
  imgFound(){
    this.foundImageInURL = true; 
     this.urlErrorElem.nativeElement.innerHTML = "";
  }
  imgNotFound(){
     this.notFoundImageInURL = true;
     this.urlErrorElem.nativeElement.innerHTML = this.MSG_URL_NOT_POINT_TO_IMG;
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
          this.urlErrorElem.nativeElement.innerHTML = this.MSG_URL_INVALID_FORMAT;
        break;
      default:
        console.error(`missing handeling of blur event for: ${formElem.name}`);
    }
  }

  /**The form has to be valid before submit. In addition the url needs to point to a valid image.
  */
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
    console.log("uploadig...");
    this.httpClient.checkNetworkAndPost<Customer>("customers", this.customer);
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
