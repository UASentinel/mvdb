import {Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {
  AgeRatingDto,
  AgeRatingsClient,
  FileParameter,
  GenreDto,
  GenresClient,
  MediaDto,
  MediasClient, SeasonsClient
} from "../../../web-api-client";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-create-season',
  templateUrl: './create-season.component.html',
  styleUrls: ['./create-season.component.css']
})
export class CreateSeasonComponent implements OnInit {
  createForm!: FormGroup;
  posterFile: File;
  mediaId: number;
  media: MediaDto;

  constructor(
    private formBuilder: FormBuilder,
    private mediasClient: MediasClient,
    private seasonsCLient: SeasonsClient,
    private currentRoute: ActivatedRoute,
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

    this.createForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: [''],
      trailerLink: [''],
      poster: [''],
      order: [1, Validators.min(1)]
    });
  }

  onSubmit(): void {
    if (this.createForm.valid) {
      const fileBlob = new Blob([this.posterFile], { type: 'image/png' });
      const file: FileParameter = { data: fileBlob, fileName: this.posterFile.name };

      this.seasonsCLient.create(
        this.createForm.value.title,
        this.createForm.value.description,
        this.createForm.value.order,
        file,
        this.createForm.value.trailerLink,
        this.mediaId
      ).subscribe(
        result => {
          this.router.navigateByUrl('manage/medias/' + this.mediaId);
        },
        error => console.error(error)
      );

    }
  }

  uploadFile(files: FileList): void {
    if (files.length === 0) {
      return;
    }
    this.posterFile = <File>files[0];
  }
}
