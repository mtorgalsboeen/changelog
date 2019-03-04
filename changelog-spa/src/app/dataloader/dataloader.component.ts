import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { CustomHttpClientService } from 'src/app/services/custom-http-client-service';
import { load } from '@angular/core/src/render3';
import { AppConfService } from 'src/app/services/app-conf.service';
/**Responsibe for showing loading progress and displaying errors. Is dependent on and used by CustomHttpLoaderService.*/
@Component({
  selector: 'dataloader',
  templateUrl: './dataloader.component.html',
  styleUrls: ['./dataloader.component.css']
})

export class DataloaderComponent implements OnInit {

  Status = StatusForRequest; //Make the enum avaible to be used in the template

  shouldShow = false;
  status = StatusForRequest.Loading;
  loadingMsg:string;
  errorMsg:string;
  noNetworkMsg:string;

  constructor(private loaderService:CustomHttpClientService) {
    this.errorMsg = AppConfService.config.uiMessages.httpRequestFailed;
    this.loadingMsg = AppConfService.config.uiMessages.httpLoading;
    this.noNetworkMsg = AppConfService.config.uiMessages.noNetwork;
    loaderService.setLoadingElem(this);
  }

  ngOnInit() {
  }

  //Events
  startLoading(){
    this.shouldShow = true;
    this.status = StatusForRequest.Loading;
  }

  doneLoading(){
    this.shouldShow = false;
  }

  noNetwork(){
    this.shouldShow = true;
    this.status = StatusForRequest.NetworkError;
  }

   errorOccured():void{
     this.status = StatusForRequest.ServerError;
  }
}

export enum StatusForRequest{
   Loading, ServerError, NetworkError
 }
