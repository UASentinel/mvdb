import {Component, OnInit} from '@angular/core';
import {EpisodeDto, EpisodesClient} from "../../../web-api-client";
import {ActivatedRoute, Router} from "@angular/router";
import {Constants} from "../../../../assets/constants";

@Component({
  selector: 'app-delete-episode',
  templateUrl: './delete-episode.component.html',
  styleUrls: ['./delete-episode.component.css']
})
export class DeleteEpisodeComponent implements OnInit {
  episodeId: number;
  episode: EpisodeDto;

  constructor(
    private currentRoute: ActivatedRoute,
    private episodesClient: EpisodesClient,
    private router: Router
  ) {}

  ngOnInit() {
    this.episodeId = Number(this.currentRoute.snapshot.paramMap.get(Constants.IdParameter));
    this.episodesClient.get(this.episodeId).subscribe(
      result => {
        this.episode = result;
      },
      error => console.error(error)
    );
  }

  onSubmit(): void {
    this.episodesClient.delete(
      this.episodeId
    ).subscribe(
      result => {
        this.router.navigateByUrl(Constants.ManageSeasonsRoute + '/' + this.episode.seasonId);
      },
      error => console.error(error)
    );
  }
}
