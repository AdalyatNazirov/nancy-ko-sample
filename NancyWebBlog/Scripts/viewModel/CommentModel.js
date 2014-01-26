function CommentModel(data) {
    var self = this

    self.text = data.Text;
    self.id = data.ID;
    self.author = data.Author;
    self.postedAt = new Date(Date.parse(data.PostedAt));
}