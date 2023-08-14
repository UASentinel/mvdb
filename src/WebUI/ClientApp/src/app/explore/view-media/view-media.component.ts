import {Component, OnInit} from '@angular/core';
import {MediaDto, MediasClient, MediaType} from "../../web-api-client";
import {DomSanitizer, SafeResourceUrl} from "@angular/platform-browser";
import {ActivatedRoute} from "@angular/router";
import {AuthorizeService} from "../../../api-authorization/authorize.service";

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

  constructor(
    private currentRoute: ActivatedRoute,
    private mediasClient: MediasClient,
    private sanitizer: DomSanitizer,
    private authorizeService: AuthorizeService
  ) {
  }
  ngOnInit() {

    this.mediaId = Number(this.currentRoute.snapshot.paramMap.get('id'));

    this.mediasClient.get(this.mediaId).subscribe(
      result => {
        this.media = result;
        let hours = Math.floor(this.media.duration / 60);
        let minutes = this.media.duration % 60;
        this.duration = hours + 'h ' + minutes + 'm';
        this.trailerLink = this.sanitizer.bypassSecurityTrustResourceUrl(this.media.trailerLink);
      },
      error => console.error(error)
    );

    this.authorizeService.getUserRoles().subscribe(
      roles => {
        this.isAdministrator = roles && roles.findIndex(r => r === 'Administrator') !== -1;
      }
    );
  }

  protected readonly MediaType = MediaType;
}
