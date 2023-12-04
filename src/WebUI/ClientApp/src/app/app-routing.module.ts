import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
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
import {DeleteMediaComponent} from "./manage/manage-medias/delete-media/delete-media.component";
import {DeleteSeasonComponent} from "./manage/manage-seasons/delete-season/delete-season.component";
import {DeleteEpisodeComponent} from "./manage/manage-episodes/delete-episode/delete-episode.component";
import {Constants} from "../assets/constants";

export const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'explore', children: [
      { path: '', component: ExploreComponent },
      { path: 'medias/:' + Constants.IdParameter, component: ViewMediaComponent },
      { path: 'seasons/:' + Constants.IdParameter, component: ViewSeasonComponent }
    ] },
  { path: 'manage', children: [
      { path: '', component: ManageComponent },
      { path: 'medias', children: [
          { path: '', component: ManageMediasComponent },
          { path: 'reorder/seasons/:' + Constants.IdParameter, component: ReorderSeasonsComponent },
          { path: 'delete/:' + Constants.IdParameter, component: DeleteMediaComponent },
          { path: 'create', children:[
              { path: '', component: CreateMediaComponent },
              { path: 'season/:' + Constants.IdParameter, component: CreateSeasonComponent }
            ] },
          { path: 'update', children: [
              { path: ':' + Constants.IdParameter, component: UpdateMediaComponent },
              { path: 'directors/:' + Constants.IdParameter, component: UpdateDirectorsComponent },
              { path: 'actors/' + Constants.IdParameter, component: UpdateActorsComponent }
            ] },
          { path: ':' + Constants.IdParameter, component: ViewMediaComponent }
        ] },
      { path: 'seasons', children: [
          { path: ':' + Constants.IdParameter, component: ViewSeasonComponent },
          { path: 'update/:' + Constants.IdParameter, component: UpdateSeasonComponent },
          { path: 'delete/:' + Constants.IdParameter, component: DeleteSeasonComponent },
          { path: 'create/episode/:' + Constants.IdParameter, component: CreateEpisodeComponent },
          { path: 'reorder/episodes/:' + Constants.IdParameter, component: ReorderEpisodesComponent }
        ] },
      { path: 'episodes', children: [
          { path: 'update/:' + Constants.IdParameter, component: UpdateEpisodeComponent },
          { path: 'delete/:' + Constants.IdParameter, component: DeleteEpisodeComponent }
        ] }
    ] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
