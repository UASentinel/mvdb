import {Component, OnInit} from '@angular/core';
import {FormArray, FormBuilder, FormGroup, Validators} from "@angular/forms";
import {
  AgeRatingDto,
  AgeRatingsClient, GenreDto,
  GenresClient,
  MediaGenreDto,
  FileParameter,
  MediasClient,
  MediaType, SearchMediasQuery, UpdateGenresCommand,
  IUpdateGenresCommand, IMediaGenreDto
} from "../../../web-api-client";
import {Router} from "@angular/router";

@Component({
  selector: 'app-create-media',
  templateUrl: './create-media.component.html',
  styleUrls: ['./create-media.component.css']
})
export class CreateMediaComponent implements OnInit{
  createForm!: FormGroup;
  ageRatings: AgeRatingDto[];
  genres: GenreDto[];
  mediaTypes: MediaType[] = [MediaType.Movie, MediaType.Series];
  posterFile: File;
  currentMediaType: string = MediaType[this.mediaTypes[0]];

  constructor(
    private formBuilder: FormBuilder,
    private mediasClient: MediasClient,
    private genresClient: GenresClient,
    private ageRatingsClient: AgeRatingsClient,
    private router: Router
  ) { }
  ngOnInit() {
    this.genresClient.getAll().subscribe(
      result => {
        this.genres = result;
      },
      error => console.error(error)
    );

    this.ageRatingsClient.getAll().subscribe(
      result => {
        this.ageRatings = result;
      },
      error => console.error(error)
    );

    this.createForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: [''],
      mediaType: [MediaType[this.mediaTypes[0]]],
      trailerLink: [''],
      ageRating: [''],
      duration: [0, Validators.min(0)],
      releaseDate: [''],
      poster: [''],
      genres: this.formBuilder.array([])
    });
  }

  onSubmit(): void {
    if (this.createForm.valid) {
      // if (1) {
      const ageRatingIndex = this.ageRatings.findIndex(a => a.name == this.createForm.value.ageRating);
      if(ageRatingIndex === -1){
        return;
      }
      const ageRatingId = this.ageRatings[ageRatingIndex].id;
      const mediaType = MediaType[this.createForm.value.mediaType as keyof typeof MediaType];
      const releaseDate = new Date(this.createForm.value.releaseDate);
      const fileBlob = new Blob([this.posterFile], { type: 'image/png' });
      const file: FileParameter = { data: fileBlob, fileName: this.posterFile.name };

      this.mediasClient.create(
        this.createForm.value.title,
        this.createForm.value.description,
        mediaType,
        file,
        this.createForm.value.trailerLink,
        ageRatingId,
        this.createForm.value.duration,
        releaseDate
      ).subscribe(
        result => {
          const id = result;
          this.updateGenres(id);

          this.router.navigateByUrl('manage/medias/' + id);
        },
        error => console.error(error)
      );

    }
  }

  onMediaTypeChange(): void {
    this.currentMediaType = this.createForm.value.mediaType;
  }

  addGenre(): void {
    const genreArray = this.createForm.get('genres') as FormArray;
    genreArray.push(this.formBuilder.group({
      name: [this.genres?.[0].name, Validators.required]
    }));
  }

  removeGenre(index: number): void {
    const genreArray = this.createForm.get('genres') as FormArray;
    genreArray.removeAt(index);
  }

  genreUp(index: number): void {
    const genreArray = this.createForm.get('genres') as FormArray;
    if(index !== 0 && genreArray.length > 1){
      const genres = genreArray.value;
      [genres[index - 1], genres[index]] = [genres[index], genres[index - 1]];
      genreArray.setValue(genres);
    }
  }

  genreDown(index: number): void {
    const genreArray = this.createForm.get('genres') as FormArray;
    if(index !== genreArray.length - 1 && genreArray.length > 1){
      const genres = genreArray.value;
      [genres[index], genres[index + 1]] = [genres[index + 1], genres[index]];
      genreArray.setValue(genres);
    }
  }

  uploadFile(files: FileList): void {
    if (files.length === 0) {
      return;
    }
    this.posterFile = <File>files[0];
  }
  updateGenres(id: number): void {
    if(id !== 0){
      const command = {
        mediaId: id,
        mediaGenreDtos: []
      } as UpdateGenresCommand;

      const genreArray = this.createForm.get('genres') as FormArray;
      const mediaGenres = genreArray.value;

      for(let i = 0; i < mediaGenres.length; i++){
        const genreIndex = this.genres.findIndex(g => g.name === mediaGenres[i].name);
        const genreId = this.genres[genreIndex].id;

        const mediaGenreDto = {
          genreId: genreId,
          order: i + 1
        } as MediaGenreDto;

        command.mediaGenreDtos.push(mediaGenreDto);
      }

      this.mediasClient.updateGenres(
        id,
        command
      ).subscribe(
        result => {
          this.router.navigateByUrl('manage/medias/' + id);
        },
        error => console.error(error)
      );
    }
  }

  protected readonly MediaType = MediaType;
}
