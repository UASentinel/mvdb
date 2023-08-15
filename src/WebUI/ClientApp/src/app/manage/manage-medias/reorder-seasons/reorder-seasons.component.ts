import {Component, OnInit} from '@angular/core';
import {
  DirectorDto,
  DirectorsClient, MediaDirectorDto,
  MediaDto,
  MediasClient, MediaSeasonDto,
  SearchDirectorsQuery, SeasonBriefDto,
  UpdateDirectorsCommand, UpdateSeasonsOrderCommand
} from "../../../web-api-client";
import {FormBuilder, FormGroup} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-reorder-seasons',
  templateUrl: './reorder-seasons.component.html',
  styleUrls: ['./reorder-seasons.component.css']
})
export class ReorderSeasonsComponent implements  OnInit{
  mediaId: number;
  media: MediaDto;

  constructor(
    private formBuilder: FormBuilder,
    private currentRoute: ActivatedRoute,
    private mediasClient: MediasClient,
    private directorsClient: DirectorsClient,
    private router: Router
  ) {}
  ngOnInit() {
    this.mediaId = Number(this.currentRoute.snapshot.paramMap.get('id'));
    this.mediasClient.get(this.mediaId).subscribe(
      result => {
        this.media = result;
      },
      error => console.error(error)
    );
  }

  saveChanges(): void {
    const command = {
      mediaId: this.mediaId,
      mediaSeasonDtos: []
    } as UpdateSeasonsOrderCommand;

    for(let i = 0; i < this.media.seasons.length; i++){
      const mediaDirectorDto = {
        seasonId: this.media.seasons[i].id,
        order: i + 1
      } as MediaSeasonDto;

      command.mediaSeasonDtos.push(mediaDirectorDto);
    }

    this.mediasClient.reorderSeasons(
      this.mediaId,
      command
    ).subscribe(
      result => {
        this.router.navigateByUrl('manage/medias/' + this.mediaId);
      },
      error => console.error(error)
    );
  }


  seasonUp(index: number): void {
    if(index !== 0 && this.media.seasons.length > 1){
      [this.media.seasons[index - 1], this.media.seasons[index]] = [this.media.seasons[index], this.media.seasons[index - 1]];
    }
  }

  seasonDown(index: number): void {
    if (index !== this.media.seasons.length - 1 && this.media.seasons.length > 1) {
      [this.media.seasons[index], this.media.seasons[index + 1]] = [this.media.seasons[index + 1], this.media.seasons[index]];
    }
  }
}

