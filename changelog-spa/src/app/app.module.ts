import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppRoutingModule } from './app-routing.module';
import { AlertifyService } from './_services/alertify.service';
import { AppComponent } from './app.component';
import { BsDropdownModule } from 'ngx-bootstrap';
import { LoginComponent } from './login/login.component';
import { ChangelogComponent } from './changelog/changelog.component';
import { appRoutes } from './routes';
import { HomeComponent } from './home/home.component';
import { UsersComponent } from './users/users.component';
import { AddproductComponent } from './addproduct/addproduct.component';
import { AddcustomerComponent } from './addcustomer/addcustomer.component';
import { AuthGuard } from './_guards/auth.guard';
import { AuthService } from './_services/auth.service';
import { ErrorInterceptorProvider } from './_services/error.interceptor';

@NgModule({
   declarations: [
      AppComponent,
      LoginComponent,
      ChangelogComponent,
      HomeComponent,
      UsersComponent,
      AddproductComponent,
      AddcustomerComponent
   ],
   imports: [
      BrowserModule,
      AppRoutingModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes)
   ],
   providers: [
      AuthService,
      ErrorInterceptorProvider,
      AlertifyService,
      AuthGuard
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
