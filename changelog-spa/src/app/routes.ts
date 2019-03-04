import {Routes} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ChangelogComponent } from './changelog/changelog.component';
import { AuthGuard } from './_guards/auth.guard';
import { UsersComponent } from './users/users.component';
import { AddproductComponent } from './addproduct/addproduct.component';
import { AddcustomerComponent } from './addcustomer/addcustomer.component';

export const appRoutes: Routes = [
    { path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate: [AuthGuard],
        children: [
            { path: 'changelog', component: ChangelogComponent},
            { path: 'users', component: UsersComponent},
            { path: 'addproduct', component: AddproductComponent},
            { path: 'addcustomer', component: AddcustomerComponent},
            { path: 'home', component: HomeComponent},
        ]
    },
    { path: '**', redirectTo: '', pathMatch: 'full'},
];