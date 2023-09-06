import {Component, OnInit} from '@angular/core';
import {
  AgeRatingDto,
  AgeRatingsClient,
  FileParameter,
  GenreDto,
  GenresClient,
  MediaDto, MediaGenreDto,
  MediasClient, SeasonDto, SeasonsClient, UpdateGenresCommand
} from "../../../web-api-client";
import {FormArray, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {Constants} from "../../../../assets/constants";

@Component({
  selector: 'app-update-season',
  templateUrl: './update-season.component.html',
  styleUrls: ['./update-season.component.css']
})
export class UpdateSeasonComponent implements OnInit {
  seasonId: number;
  season: SeasonDto;
  updateForm!: FormGroup;
  posterFile: File;

  constructor(
    private formBuilder: FormBuilder,
    private currentRoute: ActivatedRoute,
    private seasonsClient: SeasonsClient,
    private router: Router
  ) {}

  ngOnInit() {
    this.seasonId = Number(this.currentRoute.snapshot.paramMap.get(Constants.IdParameter));

    this.updateForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: [''],
      trailerLink: [''],
      poster: [''],
      order: [1, Validators.min(1)],
      deletePoster: ['']
    });

    this.seasonsClient.get(this.seasonId).subscribe(
      result => {
        this.season = result;

        const values = {
          title: this.season.title,
          description: this.season.description,
          trailerLink: this.season.trailerLink,
          poster: '',
          deletePoster: false,
          order: this.season.order
        }

        this.updateForm.reset(values);
      },
      error => console.error(error)
    );
  }

  uploadFile(files: FileList): void {
    if (files.length === 0) {
      return;
    }
    this.posterFile = <File>files[0];
  }

  onSubmit(): void {
    console.log(this.updateForm.value);
    if (this.updateForm.valid) {
      let file: FileParameter;
      if(this.posterFile === undefined){
        file = undefined;
      }
      else{
        const fileBlob = new Blob([this.posterFile], { type: 'image/png' });
        file = { data: fileBlob, fileName: 'poster' };
      }

      this.seasonsClient.update(
        this.seasonId,
        this.season.id,
        this.updateForm.value.title,
        this.updateForm.value.description,
        this.updateForm.value.order,
        file,
        this.updateForm.value.deletePoster,
        this.updateForm.value.trailerLink
      ).subscribe(
        result => {
          this.router.navigateByUrl(Constants.ManageSeasonsRoute + '/' + this.seasonId);
        },
        error => console.error(error)
      );

    }
  }
}
