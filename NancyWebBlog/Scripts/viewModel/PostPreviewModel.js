function PreviewPostModel(data) {
    var self = this

    self.title = data.Title;
    self.id = data.ID;
    self.author = data.Author;
    self.postedAt = new Date(Date.parse(data.PostedAt));
    self.preBody = data.PreBody;
}
