import {Component, OnInit} from '@angular/core';
import {
  AgeRatingDto,
  AgeRatingsClient,
  FileParameter,
  GenreDto,
  GenresClient,
  MediaDto, MediaGenreDto,
  MediasClient, UpdateGenresCommand
} from "../../../web-api-client";
import {FormArray, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {Constants} from "../../../../assets/constants";

@Component({
  selector: 'app-delete-media',
  templateUrl: './delete-media.component.html',
  styleUrls: ['./delete-media.component.css']
})
export class DeleteMediaComponent implements OnInit {
  mediaId: number;
  media: MediaDto;

  constructor(
    private currentRoute: ActivatedRoute,
    private mediasClient: MediasClient,
    private router: Router
  ) {}

  ngOnInit() {
    this.mediaId = Number(this.currentRoute.snapshot.paramMap.get(Constants.IdParameter));
    this.mediasClient.get(this.mediaId).subscribe(
      result => {
        this.media = result;
      },
      error => console.error(error)
    );
  }

  onSubmit(): void {
    this.mediasClient.delete(
      this.mediaId
    ).subscribe(
      result => {
        this.router.navigateByUrl(Constants.ManageMediasRoute);
      },
      error => console.error(error)
    );
  }
}
