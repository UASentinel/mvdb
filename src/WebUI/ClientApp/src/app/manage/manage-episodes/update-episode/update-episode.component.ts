import {Component, OnInit} from '@angular/core';
import {
  EpisodeDto,
  EpisodesClient,
  UpdateEpisodeCommand
} from "../../../web-api-client";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-update-episode',
  templateUrl: './update-episode.component.html',
  styleUrls: ['./update-episode.component.css']
})
export class UpdateEpisodeComponent implements OnInit {
  episodeId: number;
  episode: EpisodeDto;
  updateForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private currentRoute: ActivatedRoute,
    private episodesClient: EpisodesClient,
    private router: Router
  ) {}

  ngOnInit() {

    this.episodeId = Number(this.currentRoute.snapshot.paramMap.get('id'));

    this.updateForm = this.formBuilder.group({
      title: ['', Validators.required],
      description: [''],
      duration: [0, Validators.min(0)],
      releaseDate: [''],
      order: [1, Validators.min(1)]
    });

    this.episodesClient.get(this.episodeId).subscribe(
      result => {
        this.episode = result;

        const values = {
          title: this.episode.title,
          description: this.episode.description,
          duration: this.episode.duration,
          releaseDate: this.formatDate(new Date(this.episode.releaseDate)),
          order: this.episode.order
        }

        this.updateForm.reset(values);
      },
      error => console.error(error)
    );
  }

  onSubmit(): void {
    if (this.updateForm.valid) {
      const command = {
        episodeId: this.episodeId,
        title: this.updateForm.value.title,
        description: this.updateForm.value.description,
        duration: this.updateForm.value.duration,
        order: this.updateForm.value.order,
        releaseDate: new Date(this.updateForm.value.releaseDate)
    } as UpdateEpisodeCommand;

      this.episodesClient.update(
        this.episodeId,
        command
      ).subscribe(
        result => {
          this.router.navigateByUrl('manage/seasons/' + this.episode.seasonId);
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
}
