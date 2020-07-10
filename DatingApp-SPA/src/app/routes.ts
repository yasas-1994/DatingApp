import {Routes, ROUTES} from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './member-list/member-list.component';
import { ListsComponent } from './lists/lists.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

// following appRotes is an array with several routings. The order of this array list is important. the
// ** (wild card) must be the last one. Because this array works as an if condition.
// here the second array item is an dummy route with children components. once we add canActivate: [AuthGuard]
// to the dummy, it i'll work for the children as well.
export const appRoutes: Routes = [
    {path: '', component: HomeComponent},
    {path: '', runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
        {path: 'member', component: MemberListComponent},
        {path: 'messages', component: MessagesComponent},
        {path: 'lists', component: ListsComponent}

    ]
       },

    {path: '**', redirectTo: '', pathMatch: 'full'}

] ;
