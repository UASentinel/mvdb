import {Component, OnInit} from '@angular/core';
import {
  AgeRatingDto,
  AgeRatingsClient, FileParameter,
  GenreDto,
  GenresClient,
  MediaDto, MediaGenreDto,
  MediasClient,
  MediaType, UpdateGenresCommand
} from "../../../web-api-client";
import {ActivatedRoute, Router} from "@angular/router";
import {FormArray, FormBuilder, FormGroup, Validators} from "@angular/forms";

@Component({
  selector: 'app-update-media',
  templateUrl: './update-media.component.html',
  styleUrls: ['./update-media.component.css']
})
export class UpdateMediaComponent implements OnInit {
  mediaId: number;
  media: MediaDto;
  updateForm!: FormGroup;
  ageRatings: AgeRatingDto[];
  genres: GenreDto[];
  mediaTypes: MediaType[] = [MediaType.Movie, MediaType.Series];
  posterFile: File;
  currentMediaType: string;
  showGenresFlag: Boolean = false;
  showGenresClass: string = 'd-none';
  constructor(
    private formBuilder: FormBuilder,
    private currentRoute: ActivatedRoute,
    private genresClient: GenresClient,
    private ageRatingsClient: AgeRatingsClient,
    private mediasClient: MediasClient,
    private router: Router
  ) {}

  ngOnInit() {
    this.mediaId = Number(this.currentRoute.snapshot.paramMap.get('id'));

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

    this.updateForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: [''],
      mediaType: [MediaType[this.mediaTypes[0]]],
      trailerLink: [''],
      ageRating: [''],
      duration: [0, Validators.min(0)],
      releaseDate: [''],
      poster: [''],
      deletePoster: [''],
      genres: this.formBuilder.array([])
    });

    this.mediasClient.get(this.mediaId).subscribe(
      result => {
        this.media = result;

        const genreArray = this.updateForm.get('genres') as FormArray;
        for(const genre of this.media.genres){
          genreArray.push(this.formBuilder.group({
            name: [genre.name, Validators.required]
          }));
        }

        const genres = genreArray.value;
        genreArray.setValue(genres);

        this.genreUp(1);
        this.genreDown(0);

        const values = {
          title: this.media.title,
          description: this.media.description,
          mediaType: MediaType[this.media.mediaType],
          trailerLink: this.media.trailerLink,
          ageRating: this.media.ageRating?.name,  // Make sure ageRating is an object with a name property
          duration: this.media.duration,
          releaseDate: this.formatDate(new Date(this.media.releaseDate)),
          poster: '',  // You might want to populate this if needed
          deletePoster: false,  // You might want to populate this if needed
          genres: this.updateForm.value.genres
        }

        // console.log(values);

        this.updateForm.reset(values);

        // const genreArray = this.updateForm.get('genres') as FormArray;
        // for(const genre of this.media.genres){
        //   genreArray.push(this.formBuilder.group({
        //     name: [genre.name, Validators.required]
        //   }));
        // }
        //
        // const genres = genreArray.value;
        // genreArray.setValue(genres);

        // const newValues = this.updateForm.value;
        // this.updateForm.reset(newValues);

        // console.log(this.updateForm.value);
        //
        // const newValues = this.updateForm.value;
        // this.updateForm.reset(newValues);

        this.currentMediaType = MediaType[this.media.mediaType];
      },
      error => console.error(error)
    );
  }

  onMediaTypeChange(): void {
    this.currentMediaType = this.updateForm.value.mediaType;
  }

  addGenre(): void {
    const genreArray = this.updateForm.get('genres') as FormArray;
    genreArray.push(this.formBuilder.group({
      name: [this.genres?.[0].name, Validators.required]
    }));
  }

  removeGenre(index: number): void {
    const genreArray = this.updateForm.get('genres') as FormArray;
    genreArray.removeAt(index);
  }

  showGenres(): void {
    const genreArray = this.updateForm.get('genres') as FormArray;
    const genres = genreArray.value;
    genreArray.setValue(genres);
    this.showGenresFlag = !this.showGenresFlag;
    this.showGenresClass = this.showGenresFlag ? '' : 'd-none';
  }

  genreUp(index: number): void {
    const genreArray = this.updateForm.get('genres') as FormArray;
    if(index !== 0 && genreArray.length > 1){
      const genres = genreArray.value;
      [genres[index - 1], genres[index]] = [genres[index], genres[index - 1]];
      genreArray.setValue(genres);
    }
  }

  genreDown(index: number): void {
    const genreArray = this.updateForm.get('genres') as FormArray;
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

  onSubmit(): void {
    console.log(this.updateForm.value);
    if (this.updateForm.valid) {
      // if (1) {
      const ageRatingIndex = this.ageRatings.findIndex(a => a.name == this.updateForm.value.ageRating);
      if(ageRatingIndex === -1){
        return;
      }
      const ageRatingId = this.ageRatings[ageRatingIndex].id;
      const mediaType = MediaType[this.updateForm.value.mediaType as keyof typeof MediaType];
      const releaseDate = new Date(this.updateForm.value.releaseDate);
      let file: FileParameter;
      if(this.posterFile === undefined){
        file = undefined;
      }
      else{
        const fileBlob = new Blob([this.posterFile], { type: 'image/png' });
        file = { data: fileBlob, fileName: 'poster' };
      }

      this.mediasClient.update(
        this.mediaId,
        this.media.id,
        this.updateForm.value.title,
        this.updateForm.value.description,
        mediaType,
        file,
        this.updateForm.value.deletePoster,
        this.updateForm.value.trailerLink,
        ageRatingId,
        this.updateForm.value.duration,
        releaseDate
      ).subscribe(
        result => {
          this.updateGenres(this.mediaId);

          this.router.navigateByUrl('manage/medias/' + this.mediaId);
        },
        error => console.error(error)
      );

    }
  }

  updateGenres(id: number): void {
    if(id !== 0){
      console.log('start');
      const command = {
        mediaId: id,
        mediaGenreDtos: []
      } as UpdateGenresCommand;

      const genreArray = this.updateForm.get('genres') as FormArray;
      const mediaGenres = genreArray.value;

      for(let i = 0; i < mediaGenres.length; i++){
        const genreIndex = this.genres.findIndex(g => g.name === mediaGenres[i].name);
        const genreId = this.genres[genreIndex].id;

        console.log(genreIndex);
        console.log(genreId);

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

  formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  protected readonly MediaType = MediaType;
}
