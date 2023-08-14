import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { HomeComponent } from './home/home.component';
import {ManageMediasComponent} from "./manage/manage-medias/manage-medias.component";
import {ManageComponent} from "./manage/manage.component";
import {ManageMediaComponent} from "./manage/manage-medias/manage-media/manage-media.component";
import {CreateMediaComponent} from "./manage/manage-medias/create-media/create-media.component";
import {UpdateMediaComponent} from "./manage/manage-medias/update-media/update-media.component";
import {UpdateDirectorsComponent} from "./manage/manage-medias/update-directors/update-directors.component";
import {UpdateActorsComponent} from "./manage/manage-medias/update-actors/update-actors.component";

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'manage', children: [
      { path: '', component: ManageComponent },
      { path: 'medias', children: [
          { path: '', component: ManageMediasComponent },
          { path: 'create', component: CreateMediaComponent },
          { path: ':id', component: ManageMediaComponent },
          { path: 'update', children: [
              { path: ':id', component: UpdateMediaComponent },
              { path: 'directors/:id', component: UpdateDirectorsComponent },
              { path: 'actors/:id', component: UpdateActorsComponent }
            ] }
        ] }
    ] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
