function PostModel(data, onRemoved) {
    var self = this

    self.title = data.Title;
    self.id = data.ID;
    self.author = data.Author;
    self.postedAt = new Date(Date.parse(data.PostedAt));
    self.preBody = data.PreBody;
    self.body = data.Body;


    self.comments = ko.observableArray();
    var mapping = $.map(data.Comments, function (comment) {
        return new CommentModel(comment);
    });
    self.comments(mapping);
}
