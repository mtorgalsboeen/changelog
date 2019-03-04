import { Injectable, ElementRef } from '@angular/core';
import { DataloaderComponent } from '../dataloader/dataloader.component';
import {HttpClient, HttpErrorResponse, HttpParams,HttpHeaders} from '@angular/common/http';
import { Observable, of, throwError, Subject } from 'rxjs';
import {delay, retryWhen,tap,  map, catchError} from 'rxjs/operators';
import { AppConfService } from './app-conf.service';
import {ConnectionService} from 'ng-connection-service';
@Injectable({
  providedIn: 'root'
})

/*For loading a resource over http. Handles errors and missing network by diplaying on a LoadingStatusComponent*/
export class CustomHttpClientService {

  private loadingStatusElem:DataloaderComponent;
  private gotNetworkListener:Subject<Boolean>;
  private isOnline = true;

  constructor(private httpClient:HttpClient, private connectionService:ConnectionService){
    this.gotNetworkListener = new Subject<Boolean>();

    this.connectionService.monitor().subscribe(isOnline => {
      this.isOnline = isOnline;

      if(isOnline && this.gotNetworkListener.observers.length > 0){
        this.gotNetworkListener.next(true);
      }

    });
  }

  /*Checks the users network and loads the requested resource if network is present.
  A sucesfull result is sent to the provided callback
  A LoadingStatusComponent instance is used to display loading status and errors related to netowrk problems loading resources
  If the network comes back online, the loading is retried*/
  checkNetworkAndGet(path:string, callback:(r:Response) => void){
    this.checkNetowrkDoAction(() => this.getFromHttp(path, callback));
  }


  checkNetworkAndPost<T>(path:string, payload:T){
    this.checkNetowrkDoAction(() => this.postToHttp<T>(path, payload, () => console.log("Done!")));
  }

  private postToHttp<T>(path:string, payload:T, doneCallback:() => void){
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    this.httpClient.post<T>(`${AppConfService.config.httpRequest.apiUrl}/${path}`, payload).subscribe();
  }

  /*If the network is online, the action is run. Othervise a callback is registered so that the process is retired when the 
  netowkr comes back on-line*/
  private checkNetowrkDoAction(action:() => void){
    if(this.isOnline){
        action();
    }
    else{
        this.loadingStatusElem.noNetwork();
        this.gotNetworkListener.subscribe(_ =>  { //Listen for when the network comes back on-line
            this.checkNetowrkDoAction(action); this.gotNetworkListener.unsubscribe()});
    }
  }

  checkNetorkAndPost(path:string){
  }

  setLoadingElem(loadingElem:DataloaderComponent){
    this.loadingStatusElem = loadingElem;
  }

  private getFromHttp(path:string, callback:(r:Response) => void){

    /*Tries to load from a path, but delays a given time before returning the result. This is so that the loading indicator has time to appear and 
    not just blink.
    If an error occurs a new request is given a specified number of times, and with a delay between each request.
    If an error still occurs, loadingElem is notified to display an error*/
    let observable = this.httpClient.get(path, { observe:'response' as 'body'})
        .pipe(
            delay(AppConfService.config.httpRequest.minLoadingMillis),
            retryWhen(err => {
              let tryNo = 1;
              return err.pipe(
                  delay(AppConfService.config.httpRequest.delayBetweenEachRetryMillis),
                  map(err => {
                      if (tryNo++ == AppConfService.config.httpRequest.numRetries) 
                          throw err; //Sends control to catchError
                       else 
                        return err; }))}), //Continues to next error

                //This will catch when a requst have been sent a specified number of times
           catchError((e:HttpErrorResponse) => this.handleError(e)));

    this.loadingStatusElem.startLoading();
    observable.subscribe( (r:Response) => { if(r.status === 200) {callback(r);this.loadingStatusElem.doneLoading()}});
    return observable;
  }


  private handleError(error:HttpErrorResponse){
    console.error(error.status);
    this.loadingStatusElem.errorOccured();
    return of([]); //The error catching-chain is broken, this is the final place the error is handled
  }


}