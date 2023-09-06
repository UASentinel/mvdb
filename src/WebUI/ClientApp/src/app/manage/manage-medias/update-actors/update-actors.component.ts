import { Component } from '@angular/core';
import {
  ActorDto, ActorsClient,
  MediaActorDto,
  MediaDto,
  MediasClient, SearchActorsQuery,
  UpdateActorsCommand
} from "../../../web-api-client";
import {FormBuilder, FormGroup} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {Constants} from "../../../../assets/constants";

@Component({
  selector: 'app-update-actors',
  templateUrl: './update-actors.component.html',
  styleUrls: ['./update-actors.component.css']
})
export class UpdateActorsComponent {
  mediaId: number;
  media: MediaDto;
  searchForm!: FormGroup;
  searchActors: ActorDto[];
  currentActors: ActorDto[];

  constructor(
    private formBuilder: FormBuilder,
    private currentRoute: ActivatedRoute,
    private mediasClient: MediasClient,
    private actorsClient: ActorsClient,
    private router: Router
  ) {}
  ngOnInit() {
    this.mediaId = Number(this.currentRoute.snapshot.paramMap.get(Constants.IdParameter));
    this.mediasClient.get(this.mediaId).subscribe(
      result => {
        this.media = result;
        this.currentActors = this.media.actors;
      },
      error => console.error(error)
    );

    this.searchForm = this.formBuilder.group({
      name: ['']
    });

    const query = {
      name: this.searchForm.value.name
    } as SearchActorsQuery;

    this.actorsClient.search(query).subscribe(
      result => {
        this.searchActors = result;
      },
      error => console.error(error)
    );
  }

  saveChanges(): void {
    const command = {
      mediaId: this.mediaId,
      mediaActorDtos: []
    } as UpdateActorsCommand;

    for(let i = 0; i < this.currentActors.length; i++){
      const mediaDirectorDto = {
        actorId: this.currentActors[i].id,
        order: i + 1
      } as MediaActorDto;

      command.mediaActorDtos.push(mediaDirectorDto);
    }

    this.mediasClient.updateActors(
      this.mediaId,
      command
    ).subscribe(
      result => {
        this.router.navigateByUrl(Constants.ManageMediasRoute + '/' + this.mediaId);
      },
      error => console.error(error)
    );
  }

  onSearchSubmit() {
    if (this.searchForm.valid) {
      const query = {
        name: this.searchForm.value.name
      } as SearchActorsQuery;

      this.actorsClient.search(query).subscribe(
        result => {
          this.searchActors = result;
        },
        error => console.error(error)
      );
    }
  }

  addActor(index: number): void {
    const actor = this.searchActors[index];
    const actorIndex = this.currentActors.findIndex(a => a.id == actor.id);
    if(actorIndex !== -1){
      return;
    }

    this.currentActors.push(actor);
  }

  removeActor(index: number): void {
    this.currentActors.splice(index, 1);
  }

  actorUp(index: number): void {
    if(index !== 0 && this.currentActors.length > 1){
      [this.currentActors[index - 1], this.currentActors[index]] = [this.currentActors[index], this.currentActors[index - 1]];
    }
  }

  actorDown(index: number): void {
    if (index !== this.currentActors.length - 1 && this.currentActors.length > 1) {
      [this.currentActors[index], this.currentActors[index + 1]] = [this.currentActors[index + 1], this.currentActors[index]];
    }
  }
}
