import {Component, OnInit} from '@angular/core';
import {AuthorizeService} from "../../api-authorization/authorize.service";
import {Subscription} from "rxjs";
import {Constants} from "../../assets/constants";

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.scss']
})
export class NavMenuComponent implements OnInit{
  isExpanded = false;
  isAdministrator: boolean = false;
  private userRolesSubscription: Subscription;

  constructor(
    private authorizeService: AuthorizeService
  ) {}

  ngOnInit(): void {
    this.authorizeService.getUserRoles().subscribe(
      roles => {
        this.isAdministrator = roles && roles.findIndex(r => r === Constants.AdministratorRoleName) !== -1;
      }
    );

    this.userRolesSubscription = this.authorizeService.userRolesSubject.subscribe((roles) => {
      this.isAdministrator = roles && roles.findIndex(r => r === Constants.AdministratorRoleName) !== -1;
    });
  }

  ngOnDestroy(): void {
    if (this.userRolesSubscription) {
      this.userRolesSubscription.unsubscribe();
    }
  }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
