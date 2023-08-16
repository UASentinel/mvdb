import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from '../api-authorization/authorize.guard';
import { HomeComponent } from './home/home.component';
import {ManageMediasComponent} from "./manage/manage-medias/manage-medias.component";
import {ManageComponent} from "./manage/manage.component";
import {CreateMediaComponent} from "./manage/manage-medias/create-media/create-media.component";
import {UpdateMediaComponent} from "./manage/manage-medias/update-media/update-media.component";
import {UpdateDirectorsComponent} from "./manage/manage-medias/update-directors/update-directors.component";
import {UpdateActorsComponent} from "./manage/manage-medias/update-actors/update-actors.component";
import {ExploreComponent} from "./explore/explore.component";
import {ViewMediaComponent} from "./explore/view-media/view-media.component";
import {CreateSeasonComponent} from "./manage/manage-seasons/create-season/create-season.component";
import {ReorderSeasonsComponent} from "./manage/manage-medias/reorder-seasons/reorder-seasons.component";
import {ViewSeasonComponent} from "./explore/view-season/view-season.component";
import {CreateEpisodeComponent} from "./manage/manage-episodes/create-episode/create-episode.component";
import {ReorderEpisodesComponent} from "./manage/manage-seasons/reorder-episodes/reorder-episodes.component";
import {UpdateSeasonComponent} from "./manage/manage-seasons/update-season/update-season.component";
import {UpdateEpisodeComponent} from "./manage/manage-episodes/update-episode/update-episode.component";

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'explore', children: [
      { path: '', component: ExploreComponent },
      { path: 'medias/:id', component: ViewMediaComponent },
      { path: 'seasons/:id', component: ViewSeasonComponent }
    ] },
  { path: 'manage', children: [
      { path: '', component: ManageComponent },
      { path: 'medias', children: [
          { path: '', component: ManageMediasComponent },
          { path: 'reorder/seasons/:id', component: ReorderSeasonsComponent },
          { path: 'create', children:[
              { path: '', component: CreateMediaComponent },
              { path: 'season/:id', component: CreateSeasonComponent }
            ] },
          { path: 'update', children: [
              { path: ':id', component: UpdateMediaComponent },
              { path: 'directors/:id', component: UpdateDirectorsComponent },
              { path: 'actors/:id', component: UpdateActorsComponent }
            ] },
          { path: ':id', component: ViewMediaComponent }
        ] },
      { path: 'episodes', children: [
          { path: 'update/:id', component: UpdateEpisodeComponent }
        ] },
      { path: 'seasons', children: [
          { path: ':id', component: ViewSeasonComponent },
          { path: 'update/:id', component: UpdateSeasonComponent },
          { path: 'create/episode/:id', component: CreateEpisodeComponent },
          { path: 'reorder/episodes/:id', component: ReorderEpisodesComponent }
        ] }
    ] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
