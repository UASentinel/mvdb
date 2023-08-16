import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {
  CreateEpisodeCommand,
  EpisodesClient,
  SeasonDto,
  SeasonsClient
} from "../../../web-api-client";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-create-episode',
  templateUrl: './create-episode.component.html',
  styleUrls: ['./create-episode.component.css']
})
export class CreateEpisodeComponent implements OnInit {
  createForm!: FormGroup;
  seasonId: number;
  season: SeasonDto;

  constructor(
    private formBuilder: FormBuilder,
    private seasonsClient: SeasonsClient,
    private episodesClient: EpisodesClient,
    private currentRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit() {
    this.seasonId = Number(this.currentRoute.snapshot.paramMap.get('id'));

    this.seasonsClient.get(this.seasonId).subscribe(
      result => {
        this.season = result;
      },
      error => console.error(error)
    );

    this.createForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: [''],
      duration: [0, Validators.min(0)],
      releaseDate: [''],
      order: [1, Validators.min(1)]
    });
  }

  onSubmit(): void {
    if (this.createForm.valid) {
      const releaseDate = new Date(this.createForm.value.releaseDate);

      const command = {
        title: this.createForm.value.title,
        description: this.createForm.value.description,
        duration: this.createForm.value.duration,
        order: this.createForm.value.order,
        releaseDate: releaseDate,
        seasonId: this.seasonId,
      } as CreateEpisodeCommand;

      this.episodesClient.create(
        command
      ).subscribe(
        result => {
          this.router.navigateByUrl('manage/seasons/' + this.seasonId);
        },
        error => console.error(error)
      );

    }
  }
}
