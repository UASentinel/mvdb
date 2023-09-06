import {Component, OnInit} from '@angular/core';
import {MediaDto, MediasClient, MediaType} from "../../web-api-client";
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {ActivatedRoute} from "@angular/router";
import {AuthorizeService} from "../../../api-authorization/authorize.service";
import {Constants} from "../../../assets/constants";

@Component({
  selector: 'app-view-media',
  templateUrl: './view-media.component.html',
  styleUrls: ['./view-media.component.css']
})
export class ViewMediaComponent implements OnInit {
  mediaId: number;
  media: MediaDto;
  trailerLink: SafeResourceUrl;
  duration: string;
  isAdministrator: boolean = false;
  episodeCount: number = 0;
  ageRatingBadge: string;

  constructor(
    private currentRoute: ActivatedRoute,
    private mediasClient: MediasClient,
    private sanitizer: DomSanitizer,
    private authorizeService: AuthorizeService
  ) {
  }
  ngOnInit() {

    this.mediaId = Number(this.currentRoute.snapshot.paramMap.get(Constants.IdParameter));

    this.mediasClient.get(this.mediaId).subscribe(
      result => {
        this.media = result;

        let hours = Math.floor(this.media.duration / 60);
        let minutes = this.media.duration % 60;

        if(hours === 0)
          this.duration = minutes + 'm';
        else
          this.duration = hours + 'h ' + minutes + 'm';

        if(this.media.mediaType === MediaType.Series) {
          this.media.seasons.forEach(season => {
            this.episodeCount += season.episodeCount;
          })
        }

        if(this.media.ageRating.minAge < 13)
          this.ageRatingBadge = 'badge-success';
        else if(this.media.ageRating.minAge < 18)
          this.ageRatingBadge = 'badge-warning';
        else
          this.ageRatingBadge = 'badge-danger';

        this.trailerLink = this.sanitizer.bypassSecurityTrustResourceUrl(this.media.trailerLink);
      },
      error => console.error(error)
    );

    this.authorizeService.getUserRoles().subscribe(
      roles => {
        this.isAdministrator = roles && roles.findIndex(r => r === Constants.AdministratorRoleName) !== -1;
      }
    );
  }

  protected readonly MediaType = MediaType;
}
