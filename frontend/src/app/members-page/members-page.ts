import { Component, computed, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthSession } from '../auth-session';
import { MembersApi } from '../members-api';
import { MemberRow, MembersList } from '../members-list/members-list';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-members-page',
  imports: [FormsModule, MembersList, RouterLink],
  templateUrl: './members-page.html',
})
export class MembersPage {
  private readonly session = inject(AuthSession);
  private readonly membersApi = inject(MembersApi);
  private readonly router = inject(Router);

  protected readonly members = signal<MemberRow[]>([]);
  protected readonly search = signal('');
  protected readonly id = signal(0);
  protected readonly name = signal('');
  protected readonly email = signal('');
  protected readonly filteredMembers = computed(() => {
    const q = this.search().toLowerCase();
    return this.members().filter((m) => m.name.toLowerCase().includes(q));
  });
  protected readonly memberCount = computed(() => this.members().length);

  constructor() {
    this.load();
  }

  logout(): void {
    this.session.clear();
    this.router.navigateByUrl('/login');
  }

  load(): void {
    const token = this.session.token();
    if (!token) {
      return;
    }
    this.membersApi.list(token).subscribe((list) => {
      this.members.set(list.map((m) => ({ id: m.id, name: m.name, active: true })));
    });
  }

  add(): void {
    const token = this.session.token();
    if (!token) {
      return;
    }
    this.membersApi
      .create(token, { id: this.id(), name: this.name(), email: this.email() })
      .subscribe((m) => {
        this.members.update((list) => [...list, { id: m.id, name: m.name, active: true }]);
      });
  }
}
