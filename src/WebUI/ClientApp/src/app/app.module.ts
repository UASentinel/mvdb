import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap/modal';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {ManageComponent} from "./manage/manage.component";
import {ManageMediasComponent} from "./manage/manage-medias/manage-medias.component";
import {CreateMediaComponent} from "./manage/manage-medias/create-media/create-media.component";
import {UpdateMediaComponent} from "./manage/manage-medias/update-media/update-media.component";
import {UpdateDirectorsComponent} from "./manage/manage-medias/update-directors/update-directors.component";
import {UpdateActorsComponent} from "./manage/manage-medias/update-actors/update-actors.component";
import {ExploreComponent} from "./explore/explore.component";
import {ViewMediaComponent} from "./explore/view-media/view-media.component";
import {CreateSeasonComponent} from "./manage/manage-seasons/create-season/create-season.component";
import {ReorderSeasonsComponent} from "./manage/manage-medias/reorder-seasons/reorder-seasons.component";
import {ViewSeasonComponent} from "./explore/view-season/view-season.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ExploreComponent,
    ViewMediaComponent,
    ViewSeasonComponent,
    ManageComponent,
    ManageMediasComponent,
    CreateMediaComponent,
    UpdateMediaComponent,
    UpdateDirectorsComponent,
    UpdateActorsComponent,
    ReorderSeasonsComponent,
    CreateSeasonComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    ApiAuthorizationModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ModalModule.forRoot()
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
