import { Component, OnInit } from '@angular/core';
import {  ChangelogService  } from '../_services/user.service';
import {  Changelog } from '../_models/changelog';

@Component({
  selector: 'app-changelog',
  templateUrl: './changelog.component.html',
  styleUrls: ['./changelog.component.css']
})
export class ChangelogComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

  loadChangelogs() {
    this.userService.getUsers().subscribe((changelogs: Changelog[]) => {
      this.changelogs = changelogs;
    })
  }

}
