import { Component, OnInit } from '@angular/core';
import {  ChangelogService  } from '../_services/user.service';
import {  Changelog } from '../_models/changelog';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-changelog',
  templateUrl: './changelog.component.html',
  styleUrls: ['./changelog.component.css']
})
export class ChangelogComponent implements OnInit {
  changelogs: Changelog[];

  constructor(private userService: ChangelogService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.loadChangelogs();
  }

  loadChangelogs() {
    this.userService.getUsers().subscribe((changelogs: Changelog[]) => {
      this.changelogs = changelogs;
    }, error => {
      this.alertify.error(error);
    });
  }
}
