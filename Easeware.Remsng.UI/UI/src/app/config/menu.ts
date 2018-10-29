// tslint:disable-next-line:no-shadowed-variable
import { ConfigModel } from '../core/interfaces/config';

// tslint:disable-next-line:no-shadowed-variable
export class MenuConfig implements ConfigModel {
	public config: any = {};

	constructor() {
		this.config = {
			header: {
				self: {},
				items: [
					{
						title: 'Dashboard',
						root: true,
						page: '/',
					},
					{
						title: 'Actions',
						root: true,
						icon: 'flaticon-menu-button',
						toggle: 'click',
						translate: 'MENU.REPORTS',
						submenu: {
							type: 'mega',
							width: '1000px',
							alignment: 'left',
							columns: [
								{
									heading: {
										heading: true,
										title: 'Information Registrations',
									},
									items: [
										{
											title: 'Ward Registration',
											tooltip: 'Non functional dummy link',
											icon: 'flaticon-map',
										},
										{
											title: 'Street Registration',
											tooltip: 'Non functional dummy link',
											icon: 'flaticon-user',
										},
										{
											title: 'Sector Registration',
											tooltip: 'Non functional dummy link',
											icon: 'flaticon-clipboard',
										},
										{
											title: 'Category Registration',
											tooltip: 'Non functional dummy link',
											icon: 'flaticon-graphic-1',
										},
										{
											title: 'Companies Registration',
											tooltip: 'Non functional dummy link',
											icon: 'flaticon-graphic-2',
										},
										{
											title: 'Items Registration',
											tooltip: 'Non functional dummy link',
											icon: 'flaticon-graphic-2',
										}
									]
								},
								{
									bullet: 'line',
									heading: {
										heading: true,
										title: 'Project Reports',
									},
									items: [
										{
											title: 'Simulate Demand Notice',
											tooltip: 'Non functional dummy link',
											icon: '',
										},
										{
											title:
												'Run Demand Notice',
											tooltip: 'Non functional dummy link',
											icon: '',
										}
									]
								},
								{
									bullet: 'line',
									heading: {
										heading: true,
										title: 'Reports/ Downloads',
									},
									items: [
										{
											title: 'Report Summary',
											tooltip: 'Non functional dummy link',
											icon: '',
										},
										{
											title:
												'Report Break Down',
											tooltip: 'Non functional dummy link',
											icon: '',
										},
										{
											title: 'Report breakdown Seperated',
											tooltip: 'Non functional dummy link',
											icon: '',
										}
									]
								},
								{
									bullet: 'line',
									heading: {
										heading: true,
										title: 'Approvals',
									},
									items: [
										{
											title: 'Demand Notice Approvals',
											tooltip: 'Non functional dummy link',
											icon: '',
										},
										{
											title: 'Reciept Approvals',
											tooltip: 'Non functional dummy link',
											icon: '',
										}
									]
								}
							]
						}
					},
					{
						title: 'Settings',
						root: true,
						icon: 'flaticon-cogwheel-1 ',
						toggle: 'click',
						translate: 'MENU.APPS',
						submenu: {
							type: 'classic',
							alignment: 'left',
							items: [
								{
									title: 'User Management',
									tooltip: 'add, remove, update users',
									icon: 'flaticon-user'
								},
								{
									title: 'Role Management',
									page: '/crud/datatable_v1',
									icon: 'flaticon-computer'
								}
							]
						}
					}
				]
			}
		}
	}
}
