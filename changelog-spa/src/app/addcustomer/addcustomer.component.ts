import { Component,ViewChild, ElementRef, TemplateRef, OnInit} from '@angular/core';
import { Customer } from './customer.model';
import { NgForm, AbstractControl } from '@angular/forms';
import { CustomHttpClientService } from '../services/custom-http-client-service';
import { ImgLoaderDirective } from './imgLoader.directive';

@Component({
  selector: 'app-add-change-customer',
  templateUrl: './addcustomer.component.html',
  styleUrls: ['./addcustomer.component.css'],
})

/**
 * Uses ngForm to add a customer.
 * The fields are validated either when the field looses focus (the onBlur-method/the blur-event) or when the user 
 * presses the submit-button
 */

export class AddcustomerComponent{
  @ViewChild("name") nameElem:ElementRef
  @ViewChild("urlInfo") urlInfoElem:ElementRef;
  @ViewChild("imgTemp") imgTemplate:TemplateRef<any>; 
  @ViewChild(ImgLoaderDirective) imgLoaderCont: ImgLoaderDirective;
  
  readonly ADDED_MSG_DURATION = 1000;
  readonly MSG_URL_INVALID_FORMAT = "Invalid url-format";
  readonly MSG_URL_LOADING_IMG = "Loading image...";
  readonly MSG_URL_NOT_POINT_TO_IMG = "The url does not point to an image";

  readonly urlRegex = /^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$/

  customer:Customer;
  nameEmpty = false;
  urlEmpty = false;

  //Since we havent tried to load the image, both are false
  triedAndFoundImgInUrl = false;
  triedAndNotFoundImgInUrl = false; 

  showAddedMsg = false;

  constructor(private httpClient:CustomHttpClientService) { 
    if(this.customer == null)
      this.customer = new Customer();
  }

  ngOnInit(){
    this.insertImageElem();
  }

  insertImageElem(){
    this.imgLoaderCont.viewContainerRef.createEmbeddedView(this.imgTemplate);

  }

  //When the image-element enters/leaves the DOM
   imgOnViewportChange(imgInViewport:boolean){
     if(this.triedAndFoundImgInUrl)
      this.urlInfoElem.nativeElement.innerHTML = "";
     else
      this.urlInfoElem.nativeElement.innerHTML = imgInViewport ? this.MSG_URL_LOADING_IMG : "";
  }
  
  //See the asociated template-file for more information*/
  imgLoaded(){
    this.triedAndFoundImgInUrl = true; 
    this.triedAndNotFoundImgInUrl = false;
    this.urlInfoElem.nativeElement.innerHTML = "";
  }

  imgFailedToLoad(){
    var notRetriedToLoadImage = !this.triedAndNotFoundImgInUrl;
    if(notRetriedToLoadImage){ //to avoid constantly readding the image
      //Re-adds the image-loader
      this.imgLoaderCont.viewContainerRef.clear();
      this.insertImageElem();

      this.triedAndNotFoundImgInUrl = true;
      this.triedAndFoundImgInUrl = false;
    }
    else{
      this.urlInfoElem.nativeElement.innerHTML = this.MSG_URL_NOT_POINT_TO_IMG;
    }
  }


  //Validates and possibly show an error for the input when it looses focus 
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

    if(form.valid && this.triedAndFoundImgInUrl){
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
