import {Component, OnInit} from '@angular/core';
import {ActivatedRoute, Router} from "@angular/router";
import {
  DirectorDto, DirectorsClient, MediaDirectorDto,
  MediaDto, MediaGenreDto,
  MediasClient,
  MediaType,
  SearchDirectorsQuery,
  SearchMediasQuery, UpdateDirectorsCommand, UpdateGenresCommand
} from "../../../web-api-client";
import {FormArray, FormBuilder, FormGroup} from "@angular/forms";

@Component({
  selector: 'app-update-directors',
  templateUrl: './update-directors.component.html',
  styleUrls: ['./update-directors.component.css']
})
export class UpdateDirectorsComponent implements  OnInit{
  mediaId: number;
  media: MediaDto;
  searchForm!: FormGroup;
  searchDirectors: DirectorDto[];
  currentDirectors: DirectorDto[];

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
        this.currentDirectors = this.media.directors;
      },
      error => console.error(error)
    );

    this.searchForm = this.formBuilder.group({
      name: ['']
    });

    const query = {
      name: this.searchForm.value.name
    } as SearchDirectorsQuery;

    this.directorsClient.search(query).subscribe(
      result => {
        this.searchDirectors = result;
      },
      error => console.error(error)
    );
  }

  saveChanges(): void {
    const command = {
      mediaId: this.mediaId,
      mediaDirectorDtos: []
    } as UpdateDirectorsCommand;

    for(let i = 0; i < this.currentDirectors.length; i++){
      const mediaDirectorDto = {
        directorId: this.currentDirectors[i].id,
        order: i + 1
      } as MediaDirectorDto;

      command.mediaDirectorDtos.push(mediaDirectorDto);
    }

    this.mediasClient.updateDirectors(
      this.mediaId,
      command
    ).subscribe(
      result => {
        this.router.navigateByUrl('manage/medias/' + this.mediaId);
      },
      error => console.error(error)
    );
  }

  onSearchSubmit() {
    if (this.searchForm.valid) {
      const query = {
        name: this.searchForm.value.name
      } as SearchDirectorsQuery;

      this.directorsClient.search(query).subscribe(
        result => {
          this.searchDirectors = result;
        },
        error => console.error(error)
      );
    }
  }

  addDirector(index: number): void {
    const director = this.searchDirectors[index];
    const directorIndex = this.currentDirectors.findIndex(d => d.id == director.id);
    if(directorIndex !== -1){
      return;
    }

    this.currentDirectors.push(director);
  }

  removeDirector(index: number): void {
    this.currentDirectors.splice(index, 1);
  }

  directorUp(index: number): void {
    if(index !== 0 && this.currentDirectors.length > 1){
      [this.currentDirectors[index - 1], this.currentDirectors[index]] = [this.currentDirectors[index], this.currentDirectors[index - 1]];
    }
  }

  directorDown(index: number): void {
    if (index !== this.currentDirectors.length - 1 && this.currentDirectors.length > 1) {
      [this.currentDirectors[index], this.currentDirectors[index + 1]] = [this.currentDirectors[index + 1], this.currentDirectors[index]];
    }
  }
}
