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

  constructor(
    private formBuilder: FormBuilder,
    private mediasClient: MediasClient,
    private genresClient: GenresClient,
    private ageRatingsClient: AgeRatingsClient
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
      mediaType: [''],
      trailerLink: [''],
      ageRating: [''],
      duration: [0, Validators.min(1)],
      releaseDate: [''],
      poster: [''],
      genres: this.formBuilder.array([])
    });
  }

  uploadFile(files) {
    if (files.length === 0) {
      return;
    }
    this.posterFile = <File>files[0];
  }

  onSubmit(){
    if (this.createForm.valid) {
      const ageRatingIndex = this.ageRatings.findIndex(a => a.name == this.createForm.value.ageRating);
      const ageRatingId = this.ageRatings[ageRatingIndex].id;
      const mediaType = this.mediaTypes.indexOf(this.createForm.value.mediaType);
      const releaseDate = new Date(this.createForm.value.releaseDate);
      const fileBlob = new Blob([this.posterFile], { type: 'image/png' });
      const file: FileParameter = { data: fileBlob, fileName: this.posterFile.name };

      let id = 0;

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
          id = result;
        },
        error => console.error(error)
      );

      if(id !== 0){
        console.log(1);
        let command: IUpdateGenresCommand = {
          mediaId: id,
          mediaGenreDtos: []
        };

        const genreArray = this.createForm.get('genres') as FormArray;
        const mediaGenres = genreArray.value;

        for(let i = 0; i < mediaGenres.length; i++){
          const genreIndex = this.genres.findIndex(g => g.name === mediaGenres[i].name);
          const genreId = this.genres[genreIndex].id;

          let mediaGenreDto: IMediaGenreDto = {
            genreId: genreId,
            order: i + 1
          }

          command.mediaGenreDtos.push(new MediaGenreDto(mediaGenreDto));
        }

        this.mediasClient.updateGenres(
          id,
          new UpdateGenresCommand(command)
        ).subscribe(
          result => {

          },
          error => console.error(error)
        );
      }
    }
  }

  addGenre() {
    const genreArray = this.createForm.get('genres') as FormArray;
    genreArray.push(this.formBuilder.group({
      name: [this.genres?.[0].name, Validators.required]
    }));
  }

  removeGenre(index: number) {
    const genreArray = this.createForm.get('genres') as FormArray;
    genreArray.removeAt(index);
  }

  genreUp(index: number) {
    const genreArray = this.createForm.get('genres') as FormArray;
    if(index !== 0 && genreArray.length > 1){
      const genres = genreArray.value;
      [genres[index - 1], genres[index]] = [genres[index], genres[index - 1]];
      genreArray.setValue(genres);
    }
  }

  genreDown(index: number) {
    const genreArray = this.createForm.get('genres') as FormArray;
    if(index !== genreArray.length - 1 && genreArray.length > 1){
      const genres = genreArray.value;
      [genres[index], genres[index + 1]] = [genres[index + 1], genres[index]];
      genreArray.setValue(genres);
    }
  }

  protected readonly MediaType = MediaType;
}
