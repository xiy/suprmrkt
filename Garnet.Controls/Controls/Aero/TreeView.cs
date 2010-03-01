// ------------------------------------------------------------------------------
// Copyright (c) 2007-2008 Mark Anthony Gibbins (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $Author: xiy3x0 $
//  $Revision: 8 $
//  $Id: TreeView.cs 8 2008-03-02 15:54:03Z xiy3x0 $
// ------------------------------------------------------------------------------
/*
* VISTA CONTROLS FOR .NET 2.0
* 
* Written by Marco Minerva, mailto:marco.minerva@gmail.com
* 
* This code is released under the Microsoft Community License (Ms-CL).
* A copy of this license is available at
* http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedcommunitylicense.mspx
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

namespace Pyramid.Garnet.Controls.Aero
{
	[ToolboxBitmap(typeof(TreeView))]
	public class TreeView : System.Windows.Forms.TreeView
	{
		private bool _showFiles = true;
		private ImageList _imageList = new ImageList();
		private Hashtable _systemIcons = new Hashtable();
		private const int Folder = 0;
		private NativeMethods.SHFILEINFO shInfo = new NativeMethods.SHFILEINFO();

		public TreeView()
		{
			base.HotTracking = true;
			base.ShowLines = false;
			base.ImageList = _imageList;
			base.ImageList.ColorDepth = ColorDepth.Depth32Bit;
			base.MouseDown += new MouseEventHandler(TreeView_MouseDown);
			base.BeforeExpand += new TreeViewCancelEventHandler(TreeView_BeforeExpand);
			base.BeforeCollapse += new TreeViewCancelEventHandler(TreeView_BeforeCollapse);
			Bitmap b = Pyramid.Garnet.Controls.Properties.Resources.FolderClosed;
			Icon folderIcon = Icon.FromHandle(b.GetHicon());

			_imageList.Images.Add(folderIcon);
			_systemIcons.Add(TreeView.Folder, 0);
		}

		private Icon GetShellFileIcon(string filePath)
		{
			NativeMethods.SHGetFileInfo(filePath, 0, ref shInfo, (uint)Marshal.SizeOf(shInfo),
									NativeMethods.SHGFI_ICON | NativeMethods.SHGFI_SMALLICON);
			Icon icon = Icon.FromHandle(this.shInfo.hIcon);
			return icon;
		}

		#region File System TreeView Code
		void TreeView_MouseDown(object sender, MouseEventArgs e)
		{
			TreeNode node = this.GetNodeAt(e.X, e.Y);
			
			if (node == null)
				return;

			this.SelectedNode = node;
		}

		void TreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node is FileNode) return;

			DirectoryNode node = (DirectoryNode)e.Node;

			if (!node.Loaded)
			{
				GC.Collect();
				//remove the fake child node used for virtualization
				node.Nodes[0].Remove();
				node.LoadDirectory();
				if (this._showFiles == true)
					node.LoadFiles();
			}
		}

		void TreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Nodes.Count > 0)
			{
				for (int i = 0; i < e.Node.Nodes.Count; i++)
				{
					_systemIcons.Remove(e.Node.Nodes[i].Text);
					_imageList.Images.RemoveByKey(e.Node.Nodes[i].Text);
				}
				e.Node.Nodes.Clear();
				e.Node.Nodes.Add(new FakeChildNode());
			}

		}

		public void LoadPaths(string[] paths)
		{
			Bitmap b = Pyramid.Garnet.Controls.Properties.Resources.FolderClosed;
			Icon folderIcon = Icon.FromHandle(b.GetHicon());

			_imageList.Images.Add(folderIcon);
			_systemIcons.Add(TreeView.Folder, 0);

			for (int i = 0; i < paths.Length; i++)
			{
				DirectoryNode node = new DirectoryNode(this, new DirectoryInfo(paths[i]));
			}
		}

		public void LoadPath(string directoryPath)
		{
			if (System.IO.Directory.Exists(directoryPath) == false)
				throw new System.IO.DirectoryNotFoundException("directoryPath is invalid.");

			DirectoryNode node = new DirectoryNode(this, new DirectoryInfo(directoryPath));
		}

		[Serializable]
		public class DirectoryNode : TreeNode
		{
			private DirectoryInfo _directoryInfo;
			internal DirectoryNode(DirectoryInfo directoryInfo)
				: base(directoryInfo.Name)
			{
				if (directoryInfo == null)
				{
					throw new ArgumentNullException(directoryInfo.ToString());
				}
				this._directoryInfo = directoryInfo;

				this.ImageIndex = TreeView.Folder;
				this.SelectedImageIndex = this.ImageIndex;
			}

			internal DirectoryNode(TreeView treeView, DirectoryInfo directoryInfo)
				: base(directoryInfo.Name)
			{
				this._directoryInfo = directoryInfo;
				this.ImageIndex = TreeView.Folder;
				this.SelectedImageIndex = this.ImageIndex;
				this.Text = directoryInfo.FullName;
				treeView.Nodes.Add(this);
				Virtualize();
			}

			void Virtualize()
			{
				int fileCount = 0;

				try
				{
					if (this.TreeView.ShowFiles == true)
						fileCount = this._directoryInfo.GetFiles().Length;

					if ((fileCount + this._directoryInfo.GetDirectories().Length) > 0)
						this.Nodes.Add(new FakeChildNode());
				}
				catch (UnauthorizedAccessException)
				{
				}
			}

			public void LoadDirectory()
			{
				foreach (DirectoryInfo directoryInfo in _directoryInfo.GetDirectories())
				{
					DirectoryNode dr = new DirectoryNode(directoryInfo);
					dr.Tag = directoryInfo.FullName;
					this.Nodes.Add(dr);
					dr.Virtualize();
				}
			}

			public void LoadFiles()
			{
				foreach (FileInfo file in _directoryInfo.GetFiles())
				{
					FileNode fn = new FileNode(this, file);
					fn.Tag = file.FullName;
					this.Nodes.Add(fn);
				}
			}

			public bool Loaded
			{
				get
				{
					if (this.Nodes.Count != 0)
					{
						if (this.Nodes[0] is FakeChildNode)
							return false;
					}

					return true;
				}
			}

			public new TreeView TreeView
			{
				get { return (TreeView)base.TreeView; }
			}
		}

		[Serializable]
		public class FileNode : TreeNode
		{
			private FileInfo _fileInfo;
			private DirectoryNode _directoryNode;

			public FileNode(DirectoryNode directoryNode, FileInfo fileInfo)
				: base(fileInfo.Name)
			{
				if (fileInfo == null)
				{
					throw new ArgumentNullException(fileInfo.ToString());
				}
				if (directoryNode != null)
				{
					this._directoryNode = directoryNode;
				}
				this._fileInfo = fileInfo;
				this.ImageIndex = ((TreeView)_directoryNode.TreeView).GetIconImageIndex(_fileInfo.FullName);
				this.SelectedImageIndex = this.ImageIndex;
			}
		}

		[Serializable]
		public class FakeChildNode : TreeNode
		{
			public FakeChildNode()
				: base()
			{
			}
		}

		public int GetIconImageIndex(string path)
		{
			string file = Path.GetFileName(path);

			if (_systemIcons.ContainsKey(file) == false)
			{
				Icon icon = GetShellFileIcon(path);
				_imageList.Images.Add(file, icon);
				_systemIcons.Add(file, _imageList.Images.Count - 1);
			}

			//return (int)_systemIcons[Path.GetExtension(path)];
			return _imageList.Images.IndexOfKey(file);
		}

		public bool ShowFiles
		{
			get { return this._showFiles; }
			set { this._showFiles = value; }
		}

		#endregion

		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.Style |= NativeMethods.TVS_NOHSCROLL;
				return cp;
			}
		}

		public new bool HotTracking
		{
			get { return base.HotTracking; }
			set { base.HotTracking = true; }
		}

		public new bool ShowLines
		{
			get { return base.ShowLines; }
			set { base.ShowLines = false; }
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);

			int style = NativeMethods.SendMessage(base.Handle, Convert.ToUInt32(NativeMethods.TVM_GETEXTENDEDSTYLE), 0, 0);
			style |= (NativeMethods.TVS_EX_AUTOHSCROLL);
			NativeMethods.SendMessage(base.Handle, NativeMethods.TVM_SETEXTENDEDSTYLE, 0, style);
			NativeMethods.SetWindowTheme(base.Handle, "explorer", null);
		}
	}
}
