import {Component, OnInit} from '@angular/core';
import {
  DirectorsClient,
  MediaDto,
  MediasClient,
  MediaSeasonDto, SeasonDto, SeasonEpisodeDto, SeasonsClient, UpdateEpisodesOrderCommand,
  UpdateSeasonsOrderCommand
} from "../../../web-api-client";
import {FormBuilder} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-reorder-episodes',
  templateUrl: './reorder-episodes.component.html',
  styleUrls: ['./reorder-episodes.component.css']
})
export class ReorderEpisodesComponent implements  OnInit{
  seasonId: number;
  season: SeasonDto;

  constructor(
    private formBuilder: FormBuilder,
    private currentRoute: ActivatedRoute,
    private seasonsClient: SeasonsClient,
    private directorsClient: DirectorsClient,
    private router: Router
  ) {}
  ngOnInit() {
    this.seasonId = Number(this.currentRoute.snapshot.paramMap.get('id'));
    this.seasonsClient.get(this.seasonId).subscribe(
      result => {
        this.season = result;
      },
      error => console.error(error)
    );
  }

  saveChanges(): void {
    const command = {
      seasonId: this.seasonId,
      seasonEpisodeDtos: []
    } as UpdateEpisodesOrderCommand;

    for(let i = 0; i < this.season.episodes.length; i++){
      const seasonEpisodeDto = {
        episodeId: this.season.episodes[i].id,
        order: i + 1
      } as SeasonEpisodeDto;

      command.seasonEpisodeDtos.push(seasonEpisodeDto);
    }

    this.seasonsClient.reorderEpisodes(
      this.seasonId,
      command
    ).subscribe(
      result => {
        this.router.navigateByUrl('manage/seasons/' + this.seasonId);
      },
      error => console.error(error)
    );
  }


  episodeUp(index: number): void {
    if(index !== 0 && this.season.episodes.length > 1){
      [this.season.episodes[index - 1], this.season.episodes[index]] = [this.season.episodes[index], this.season.episodes[index - 1]];
    }
  }

  episodeDown(index: number): void {
    if (index !== this.season.episodes.length - 1 && this.season.episodes.length > 1) {
      [this.season.episodes[index], this.season.episodes[index + 1]] = [this.season.episodes[index + 1], this.season.episodes[index]];
    }
  }
}
