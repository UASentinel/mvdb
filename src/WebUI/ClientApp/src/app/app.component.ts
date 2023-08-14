import {Component, OnInit} from '@angular/core';
import {AuthenticationResultStatus, AuthorizeService} from "../api-authorization/authorize.service";
import {ActivatedRoute, Router} from "@angular/router";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
}
